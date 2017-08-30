using System;
using Warner.Analyzer.CommandLine;
using Warner.Api.Configuration;

namespace Warner.Analyzer
{
    public class ApplicationConfiguration
    {
        public ApplicationConfiguration(
            CommandLineOptions commandLineOptions,
            WarnerApiConfiguration apiConfig)
        {
            CommandLineOptions = commandLineOptions;
            ApiConfiguration = apiConfig;
        }

        public CommandLineOptions CommandLineOptions { get; }

        public WarnerApiConfiguration ApiConfiguration { get; }
    }
}
