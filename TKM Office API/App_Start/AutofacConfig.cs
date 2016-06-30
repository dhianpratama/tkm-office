using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Core;
using Core.Services;
using Core.Services.Master;
using Microsoft.AspNet.SignalR;
using Core.Services.Sys;
using Core.Services.Tkm;
using Microsoft.Owin.Security.OAuth;
using Service;
using Service.Master;
using Service.Sys;
using Service.Tkm;
using TKM_Office_API.Security;

namespace TKM_Office_API
{
    public class AutofacConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<SimpleAuthorizationServerProvider>();
            builder.RegisterType<WebApiAuthorizeAttribute>().PropertiesAutowired();
            RegisterServices(builder);
            ServiceConfig.RegisterRepositories(builder);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<SecurityService>().As<ISecurityService>().InstancePerLifetimeScope();
            builder.RegisterType<InstitutionService>().As<IInstitutionService>().InstancePerLifetimeScope();
            builder.RegisterType<UomService>().As<IUomService>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();
            builder.RegisterType<LocationTypeService>().As<ILocationTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<LocationService>().As<ILocationService>().InstancePerLifetimeScope();
            builder.RegisterType<ShelveService>().As<IShelveService>().InstancePerLifetimeScope();
            //builder.RegisterType<BroadcastService>()
            //    .WithParameter(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof (IHubContext), (pi, ctx) => ctx.ResolveNamed<IHubContext>("TransactionQuantityHub")))
            //    .As<IBroadcastService>().InstancePerLifetimeScope();
            builder.RegisterType<ReaderModuleService>().As<IReaderModuleService>().InstancePerLifetimeScope();
            builder.RegisterType<BinService>().As<IBinService>().InstancePerLifetimeScope();
            builder.RegisterType<BrandService>().As<IBrandService>().InstancePerLifetimeScope();
            builder.RegisterType<SysConfigurationService>().As<ISysConfigurationService>().InstancePerLifetimeScope();
            //builder.RegisterType<StockCardService>().As<IStockCardService>().InstancePerLifetimeScope();
            //builder.RegisterType<StockCardOutOfStockService>().As<IStockCardOutOfStockService>().InstancePerLifetimeScope();
            builder.RegisterType<UserInstitutionRoleService>().As<IUserInstitutionRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionReportService>().As<ITransactionReportService>().InstancePerLifetimeScope();
        }
    }
}