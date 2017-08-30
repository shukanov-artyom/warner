using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries
{
    public class GetProjectQueryContext : IQueryContext
    {
        public GetProjectQueryContext(IProjectService projectService)
        {
            Projects = projectService;
        }

        public IProjectService Projects
        {
            get;
        }
    }
}
