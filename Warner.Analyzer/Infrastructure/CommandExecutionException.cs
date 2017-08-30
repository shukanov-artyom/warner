using System;

namespace Warner.Analyzer.Infrastructure
{
    public class CommandExecutionException : Exception
    {
        public CommandExecutionException(string commandName, string message)
            : base(message)
        {
            this.CommandName = commandName;
        }

        public string CommandName { get; private set; }
    }
}
