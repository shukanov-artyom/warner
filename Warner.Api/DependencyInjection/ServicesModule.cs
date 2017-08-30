using Autofac;
using Warner.Api.Services;

namespace Warner.Api.DependencyInjection
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WarningService>()
                .As<IWarningService>().InstancePerLifetimeScope();
            builder.RegisterType<BuildService>()
                .As<IBuildService>();
            builder.RegisterType<ProjectService>()
                .As<IProjectService>();

            // Unit of work must be instance per request
            // so we have explicit control over database update moment.
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
