using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace SignalR
{
    public class SignalRConfig
    {
        public static void RegisterHubs(ContainerBuilder builder)
        {
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
            //builder.Register(c => ((IConnectionManager)c.Resolve<HubConfiguration>().Resolver.GetService(typeof(IConnectionManager))).GetHubContext<ItemQuantityHub>())
            //    .Named<IHubContext>(typeof(ItemQuantityHub).Name).ExternallyOwned();
            //builder.RegisterType<MockItemQuantity>().WithParameter(ResolvedParameter.ForNamed<IHubContext>(typeof(ItemQuantityHub).Name)).SingleInstance();
        }
    }
}
