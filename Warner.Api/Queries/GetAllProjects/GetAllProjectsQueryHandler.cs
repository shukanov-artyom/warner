using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IAsyncQueryHandler<GetAllProjectsQuery>
    {
        private readonly IProjectService projectService;

        public GetAllProjectsQueryHandler(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public Task<IQueryResult> GetColdTask(GetAllProjectsQuery query)
        {
            var context = new GetAllProjectsQueryContext(projectService);
            return new Task<IQueryResult>(() => query.Run(context));
        }
    }
}
