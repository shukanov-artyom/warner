using System;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Queries.BuildWarningSummary
{
    public class BuildWarningSummaryQuery : IQuery
    {
        private readonly long buildId;

        public BuildWarningSummaryQuery(long buildId)
        {
            this.buildId = buildId;
        }

        public IQueryResult Run(IQueryContext context)
        {
            BuildWarningSummaryQueryContext typedContext =
                context as BuildWarningSummaryQueryContext;
            var summary = typedContext.Warnings.GetSummaryForBuild(buildId);
            return new BuildWarningSummaryQueryResult(summary);
        }
    }
}
