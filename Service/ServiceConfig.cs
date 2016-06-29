using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Core;
using EF;
using SignalR;

namespace Service
{
    public class ServiceConfig
    {
        public static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (EfRepository<>)).As(typeof (IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<EfUnitOfWork>().As(typeof (IUnitOfWork)).InstancePerLifetimeScope();
            builder.RegisterType<SmartShelveContext>();
            builder.RegisterType<SpWrapper>().As<ISpWrapper>().InstancePerLifetimeScope();
            SignalRConfig.RegisterHubs(builder);
        }
    }
}