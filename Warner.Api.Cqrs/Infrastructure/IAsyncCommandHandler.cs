using System;
using System.Threading.Tasks;
using Warner.Api.Infrastructure.Cqrs;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface IAsyncCommandHandler<TCommand>
        where TCommand : ICommand 
    {
        Task<ICommandResult> ExecuteAsync(TCommand command);

        Task<ICommandResult> GetColdTask(TCommand command);
    }
}
