using System;
using Autofac;
using Warner.Api.Gateway;
using Warner.Reportage.DomainServices;
using Warner.Reportage.DomainServices.Implementation;

namespace Warner.Reportage.DependencyInjection
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebApiWarnerService>()
                .As<IWarnerService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<WebApiWarnerReportingService>()
                .As<IWarnerReportingService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RemoteProjectsService>()
                .As<IProjectsService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RemoteBuildsService>()
                .As<IBuildsService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RemoteWarningService>()
                .As<IWarningService>()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
