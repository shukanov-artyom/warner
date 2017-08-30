using System;
using Warner.Api.Infrastructure.Cqrs;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface ICommand
    {
        ICommandResult Execute(ICommandContext commandContext);
    }
}
