using System;
using Warner.Api.Commands.AddProject;
using Warner.Api.Commands.WarningReport;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Test.Cqrs.Commands
{
    internal class TestCommandDispatcher
    {
        public ICommandResult Submit<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command is AddProjectCommand)
            {
                var commandHandler = new AddProjectCommandTestHandler();
                return commandHandler.Handle(command as AddProjectCommand);
            }
            if (command is WarningReportCommand)
            {
                var commandHandler = new ReportWarningCommandTestHandler();
                return commandHandler.Handle(command as WarningReportCommand);
            }
            throw new NotImplementedException(
                "Command type test support is not implemented yet in the test framework.");
        }
    }
}
