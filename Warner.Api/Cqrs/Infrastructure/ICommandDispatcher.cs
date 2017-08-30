using System;
using System.Threading.Tasks;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface ICommandDispatcher
    {
        Task<ICommandResult> Submit<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
