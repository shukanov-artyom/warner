using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Commands.AddProject
{
    public class AddProjectCommand :
        ICommand<AddProjectCommandContext, AddProjectCommandResult>
    {
        public AddProjectCommand(Project project)
        {
            Project = project ?? throw new ArgumentNullException(nameof(project));
        }

        public Project Project { get; }

        public AddProjectCommandResult Execute(AddProjectCommandContext context)
        {
            context.ProjectService.SaveNew(Project);
            context.UnitOfWork.SubmitChanges();
            Project persistedOne = context.ProjectService.GetByName(Project.Name);
            return new AddProjectCommandResult(persistedOne);
        }

        public ICommandResult Execute(ICommandContext commandContext)
        {
            return Execute(commandContext as AddProjectCommandContext);
        }
    }
}
