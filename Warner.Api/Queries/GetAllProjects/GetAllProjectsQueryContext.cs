using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.GetAllProjects
{
    public class GetAllProjectsQueryContext : IQueryContext
    {
        public GetAllProjectsQueryContext(
            IProjectService projectService)
        {
            Projects = projectService
                ?? throw new ArgumentNullException(nameof(projectService));
        }

        public IProjectService Projects
        {
            get;
        }
    }
}
