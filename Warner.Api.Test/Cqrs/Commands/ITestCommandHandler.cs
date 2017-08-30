using System;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Test.Cqrs.Commands
{
    internal interface ITestCommandHandler<in TCommand>
        where TCommand : ICommand
    {
        ICommandResult Handle(TCommand command);
    }
}
