using System;
using Warner.Analyzer.Commands;

namespace Warner.Analyzer.Infrastructure
{
    public class Application : IApplication
    {
        private readonly ApplicationCommand command;

        public Application(
            ApplicationCommand command,
            ApplicationConfiguration appConfiguration)
        {
            this.command = command;
        }

        public void Run()
        {
            CommandResult result = command.Execute();
            if (!result.IsSuccess)
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }
    }
}
