using System;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Queries.GetAllWarningsForBuild
{
    public class GetAllWarningsForBuildQuery : IQuery
    {
        private readonly long buildId;

        public GetAllWarningsForBuildQuery(long buildId)
        {
            this.buildId = buildId;
        }

        public IQueryResult Run(IQueryContext context)
        {
            GetAllWarningsForBuildQueryContext typedContext =
                context as GetAllWarningsForBuildQueryContext;
            var warnings = typedContext.Warnings.GetForBuild(buildId);
            return new GetAllWarningsForBuildQueryResult(warnings);
        }
    }
}
