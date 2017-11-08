using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Commands.AddBuild
{
    public class AddBuildCommandHandler :
        IAsyncCommandHandler<ICommand<AddBuildCommandContext, AddBuildCommandResult>>
    {
        private readonly IBuildService buildservice;
        private readonly IUnitOfWork unitOfWork;

        public AddBuildCommandHandler(
            IBuildService buildservice,
            IUnitOfWork unitOfWork)
        {
            this.buildservice = buildservice;
            this.unitOfWork = unitOfWork;
        }

        public Task<ICommandResult> ExecuteAsync(
            ICommand<AddBuildCommandContext,
                AddBuildCommandResult> command)
        {
            throw new NotImplementedException();
        }

        public Task<ICommandResult> GetColdTask(
            ICommand<AddBuildCommandContext,
                AddBuildCommandResult> command)
        {
            var context = new AddBuildCommandContext(buildservice, unitOfWork);
            return new Task<ICommandResult>(() => command.Execute(context));
        }
    }
}
