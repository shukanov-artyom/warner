using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Commands.AddProject
{
    public class AddProjectCommandHandler :
        IAsyncCommandHandler<ICommand<AddProjectCommandContext, AddProjectCommandResult>>
    {
        private IProjectService service;
        private IUnitOfWork unitOfWork;

        public AddProjectCommandHandler(
            IProjectService service,
            IUnitOfWork unitOfWork)
        {
            this.service = service;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> ExecuteAsync(
            ICommand<AddProjectCommandContext, AddProjectCommandResult> command)
        {
            var commandContext = new AddProjectCommandContext(service, unitOfWork);
            return await Task.Run(() => command.Execute(commandContext));
        }

        public Task<ICommandResult> GetColdTask(
            ICommand<AddProjectCommandContext, AddProjectCommandResult> command)
        {
            var commandContext = new AddProjectCommandContext(service, unitOfWork);
            return new Task<ICommandResult>(() => command.Execute(commandContext));
        }
    }
}
