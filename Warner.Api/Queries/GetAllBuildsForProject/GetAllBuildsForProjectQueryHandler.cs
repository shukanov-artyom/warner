using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.GetAllBuildsForProject
{
    public class GetAllBuildsForProjectQueryHandler :
        IAsyncQueryHandler<GetAllBuildsForProjectQuery>
    {
        private readonly IBuildService buildService;

        public GetAllBuildsForProjectQueryHandler(IBuildService buildService)
        {
            this.buildService = buildService;
        }

        public Task<IQueryResult> GetColdTask(GetAllBuildsForProjectQuery query)
        {
            var context = new GetAllBuildsForProjectQueryContext(buildService);
            return new Task<IQueryResult>(() => query.Run(context));
        }
    }
}
