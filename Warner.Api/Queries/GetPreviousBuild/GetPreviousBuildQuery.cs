using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries.GetPreviousBuild
{
    public class GetPreviousBuildQuery : IQuery
    {
        private readonly long buildId;

        public GetPreviousBuildQuery(long buildId)
        {
            this.buildId = buildId;
        }

        public IQueryResult Run(IQueryContext context)
        {
            var typedContext = context as GetPreviousBuildQueryContext;
            Build previousBuild = typedContext.Builds.GetPreviousFor(buildId);
            var result = new GetPreviousBuildQueryResult(previousBuild);
            return result;
        }
    }
}
