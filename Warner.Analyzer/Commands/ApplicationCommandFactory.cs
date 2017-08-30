using System;
using Warner.Analyzer.CommandLine;
using Warner.Analyzer.Report;

namespace Warner.Analyzer.Commands
{
    public class ApplicationCommandFactory
    {
        private readonly ApplicationConfiguration appConfiguration;
        private readonly IWarningReportService reportService;

        public ApplicationCommandFactory(
            ApplicationConfiguration appConfiguration,
            IWarningReportService reportService)
        {
            this.appConfiguration = appConfiguration
                ?? throw new ArgumentNullException(nameof(appConfiguration));
            this.reportService = reportService
                ?? throw new ArgumentNullException(nameof(reportService));
        }

        public ApplicationCommand Create()
        {
            if (appConfiguration.CommandLineOptions.ConsoleCommand == ApplicationCommandType.ReportBuild)
            {
                return new ApplicationCommandReportBuild(
                    appConfiguration,
                    reportService);
            }
            if (appConfiguration.CommandLineOptions.ConsoleCommand == ApplicationCommandType.GetHelp)
            {
                return new ApplicationCommandGetHelp(appConfiguration.CommandLineOptions);
            }
            throw new NotImplementedException("Command not implemented yet.");
        }
    }
}
