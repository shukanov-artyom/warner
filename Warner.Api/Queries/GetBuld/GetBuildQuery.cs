using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries.GetBuld
{
    public class GetBuildQuery : IQuery
    {
        public GetBuildQuery(long buildId)
        {
            BuildId = buildId;
        }

        private long BuildId
        {
            get;
        }

        public IQueryResult Run(IQueryContext context)
        {
            var typedContext = context as GetBuildQueryContext;
            Build build = typedContext.Builds.GetById(BuildId);
            return new GetBuildQueryResult(build) { IsSuccess = true };
        }
    }
}
