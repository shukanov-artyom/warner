using System;
using Warner.Analyzer.CommandLine;
using Warner.Analyzer.CommandLine.GetHelpCommand;

namespace Warner.Analyzer.Commands
{
    public class ApplicationCommandGetHelp : ApplicationCommand
    {
        private const string help =
@"The following commands are accepted: 
    help
    reportbuild

Details: 

    help : 

        shows current help 
    
    reportbuild :
        
        Analyzes build log and reports collected data to configured application webservice.
        
        Accepts arguments (order matters):
        [ProjectName] [BuildNumber] [LogFilePathName] [SourceCodeFolder]

        Example:
        reportbuild ORTB 2345 E:\build\ORTB\build2334-12.log d:\dev\ORTB-MERGE-WEBSITES\
        ";

        private readonly CommandLineOptionsGetHelp options;

        public ApplicationCommandGetHelp(CommandLineOptions options)
            : base(ApplicationCommandType.GetHelp)
        {
            this.options = options as CommandLineOptionsGetHelp;
            if (options == null)
            {
                throw new ArgumentException("options");
            }
        }

        public override CommandResult Execute()
        {
            Console.Write(help);
            return CommandResult.Ok;
        }
    }
}
