using System;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Warner.Persistency;

namespace Warner.Api.DependencyInjection
{
    public class PersistencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ApplicationDataContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDataContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
