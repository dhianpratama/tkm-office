using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using TKM_Office_API;
using TKM_Office_API.Security;

[assembly : OwinStartup(typeof(Startup))]
namespace TKM_Office_API
{
    public class Startup
    {
        public static HubConfiguration hubConfiguration { get; set; }
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            hubConfiguration = new HubConfiguration();
            var builder = new ContainerBuilder();
            builder.RegisterInstance(hubConfiguration).As<HubConfiguration>();
            AutofacConfig.Register(builder);
            builder.RegisterWebApiFilterProvider(httpConfiguration);
            var container = builder.Build();
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            hubConfiguration.Resolver = new AutofacDependencyResolver(container);
            ConfigureOAuth(app, container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(httpConfiguration);
            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
            app.MapSignalR("/signalr", hubConfiguration);
//            app.MapSignalR();
        }

        private static void ConfigureOAuth(IAppBuilder app, IComponentContext container)
        {
            var opt = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = container.Resolve<SimpleAuthorizationServerProvider>()
            };

            app.UseOAuthAuthorizationServer(opt);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}