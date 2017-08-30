using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Queries.GetBuld;
using Warner.Api.Services;

namespace Warner.Api.QueryHandlers
{
    public class GetBuildQueryHandler :
        IAsyncQueryHandler<GetBuildQuery>
    {
        public GetBuildQueryHandler(IBuildService buildService)
        {
            Builds = buildService;
        }

        private IBuildService Builds
        {
            get;
        }

        public Task<IQueryResult> GetColdTask(GetBuildQuery query)
        {
            GetBuildQueryContext context = new GetBuildQueryContext(Builds);
            return new Task<IQueryResult>(() => query.Run(context));
        }
    }
}
