using System;
using Warner.Analyzer.CommandLine.GetHelpCommand;

namespace Warner.Analyzer.CommandLine.OptionsFactory
{
    public static class CommandLineOptionsFactory
    {
        public static CommandLineOptions Generate(string[] args)
        {
            string cmd = args[0];
            if (cmd == ApplicationCommandType.GetHelp)
            {
                return new CommandLineOptionsGetHelp()
                {
                    ConsoleCommand = cmd,
                };
            }
            if (cmd == ApplicationCommandType.ReportBuild)
            {
                return new CommandLineOptionsReportBuild()
                {
                    ConsoleCommand = cmd,
                    ProjectName = args[1],
                    BuildNumber = long.Parse(args[2]),
                    LogFilePathName = args[3],
                    RepositoryLocalPath = args[4],
                    RepositoryType = Repository.RepositoryType.SVN
                };
            }
            throw new NotImplementedException("command not implemented");
        }
    }
}
