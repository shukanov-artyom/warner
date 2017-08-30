using System;
using Warner.Api.Infrastructure.Cqrs;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface IGenericCommand<out TCommandResult> : ICommand
        where TCommandResult : class, ICommandResult
    {
        TCommandResult Exec(ICommandContext commandContext);
    }
}
