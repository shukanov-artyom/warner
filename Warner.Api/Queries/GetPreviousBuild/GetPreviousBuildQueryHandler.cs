using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.GetPreviousBuild
{
    public class GetPreviousBuildQueryHandler :
        IAsyncQueryHandler<GetPreviousBuildQuery>
    {
        public GetPreviousBuildQueryHandler(IBuildService buildService)
        {
            Builds = buildService;
        }

        private IBuildService Builds
        {
            get;
        }

        public Task<IQueryResult> GetColdTask(GetPreviousBuildQuery query)
        {
            var context = new GetPreviousBuildQueryContext(Builds);
            return new Task<IQueryResult>(() => query.Run(context));
        }
    }
}