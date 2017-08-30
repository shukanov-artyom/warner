using System;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface ICommand
    {
        ICommandResult Execute(ICommandContext commandContext);
    }
}
