using System;
using Autofac;
using Warner.Api.Configuration;

namespace Warner.Reportage.DependencyInjection
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WarnerApiConfiguration>()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
