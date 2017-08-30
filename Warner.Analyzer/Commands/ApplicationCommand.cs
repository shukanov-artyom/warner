using System;

namespace Warner.Analyzer.Commands
{
    public abstract class ApplicationCommand
    {
        public ApplicationCommand(string commandName)
        {
            CommandName = commandName;
        }

        public string CommandName { get; private set; }

        public abstract CommandResult Execute();
    }
}
