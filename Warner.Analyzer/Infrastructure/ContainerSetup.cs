using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Warner.Analyzer.CommandLine;
using Warner.Analyzer.CommandLine.OptionsFactory;
using Warner.Analyzer.Commands;
using Warner.Analyzer.Report;
using Warner.Api.Configuration;
using Warner.Api.Gateway;

namespace Warner.Analyzer.Infrastructure
{
    /// <summary>
    /// Sets up all objects required for Application functioning.
    /// They will be delivered through DI.
    /// </summary>
    public class ContainerSetup
    {
        private IContainer container;

        public IContainer Build(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            // register configuration
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            containerBuilder.RegisterInstance<IConfigurationRoot>(configuration);

            // Registering application itself
            containerBuilder.RegisterType<Application>().As<IApplication>();

            // register incapsulated application command line
            CommandLineOptions options = CommandLineOptionsFactory.Generate(args);
            containerBuilder.RegisterInstance(options);

            // registering speedomenetr service
            containerBuilder.RegisterType<MeteredWarningReportService>()
                .As<IWarningReportService>();

            // Register main configuration sources
            containerBuilder.RegisterType<ApplicationConfiguration>();
            containerBuilder.RegisterType<WarnerApiConfiguration>();

            containerBuilder.RegisterType<ApplicationCommandFactory>();
            //            containerBuilder.RegisterType<StubWarnerService>()
            //                .As<IWarnerService>();
            containerBuilder.RegisterType<WebApiWarnerService>()
                .As<IWarnerService>();

            // Register command requested by a user
            containerBuilder.Register(c =>
                container.Resolve<ApplicationCommandFactory>().Create())
                .As<ApplicationCommand>();

            return container = containerBuilder.Build();
        }
    }
}
