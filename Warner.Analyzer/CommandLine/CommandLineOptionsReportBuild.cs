using System;
using Warner.Analyzer.Repository;

namespace Warner.Analyzer.CommandLine
{
    public class CommandLineOptionsReportBuild : CommandLineOptions
    {
        public long BuildNumber { get; set; }

        public string ProjectName { get; set; }

        public string LogFilePathName { get; set; }

        public string RepositoryLocalPath { get; set; }

        public RepositoryType RepositoryType { get; set; }
    }
}
