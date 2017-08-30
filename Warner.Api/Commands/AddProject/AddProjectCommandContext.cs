using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Commands.AddProject
{
    public class AddProjectCommandContext : ICommandContext
    {
        public AddProjectCommandContext(
            IProjectService projectService,
            IUnitOfWork unitOfWork)
        {
            ProjectService = projectService;
            UnitOfWork = unitOfWork;
        }

        public IProjectService ProjectService { get; set; }

        public IUnitOfWork UnitOfWork { get; set; }
    }
}
