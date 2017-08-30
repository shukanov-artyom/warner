using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.GetBuld
{
    public class GetBuildQueryContext : IQueryContext
    {
        public GetBuildQueryContext(
            IBuildService buildService)
        {
            Builds = buildService;
        }

        public IBuildService Builds
        {
            get;
        }
    }
}
