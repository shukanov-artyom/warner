using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Commands.AddBuild
{
    public class AddBuildCommand :
        ICommand<AddBuildCommandContext, AddBuildCommandResult>
    {
        public AddBuildCommand(Build build)
        {
            Build = build;
        }

        private Build Build { get; }

        public AddBuildCommandResult Execute(AddBuildCommandContext context)
        {
            Build resultBuild = context.BuildService.SaveNew(Build);
            return new AddBuildCommandResult(true, resultBuild.Id);
        }

        public ICommandResult Execute(ICommandContext commandContext)
        {
            throw new NotImplementedException();
        }
    }
}
