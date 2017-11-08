using Autofac;
using System.Reflection;
using Warner.Api.CommandDispatch;
using Warner.Api.Commands;
using Warner.Api.Commands.AddBuild;
using Warner.Api.Commands.AddProject;
using Warner.Api.Commands.WarningReport;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.DependencyInjection
{
    public class CqrsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // grab an assembly
            var asm = typeof(WarningReportCommand).GetTypeInfo().Assembly;

            // command dispatcher registration
            builder.RegisterType<AsyncCommandDispatcher>()
                .As<ICommandDispatcher>()
                .SingleInstance();

            // Query dispatcher registration
            builder.RegisterType<AsyncQueryDispatcher>()
                .As<IQueryDispatcher>()
                .SingleInstance();

            // Register commands
            builder.RegisterAssemblyTypes(asm)
                .AssignableTo<ICommand>();

            // register queries
            builder.RegisterAssemblyTypes(asm)
                .AssignableTo<IQuery>();

            // register command handlers
            builder.RegisterType<AddProjectCommandHandler>()
                .As<IAsyncCommandHandler<ICommand<AddProjectCommandContext, AddProjectCommandResult>>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ReportWarningCommandHandler>()
                .As<IAsyncCommandHandler<ICommand<WarningReportCommandContext, WarningReportCommandResult>>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AddBuildCommandHandler>()
                .As<IAsyncCommandHandler<ICommand<AddBuildCommandContext, AddBuildCommandResult>>>()
                .InstancePerLifetimeScope();

            // register query handlers
            builder.RegisterAssemblyTypes(asm)
                .AsClosedTypesOf(typeof(IAsyncQueryHandler<>))
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
