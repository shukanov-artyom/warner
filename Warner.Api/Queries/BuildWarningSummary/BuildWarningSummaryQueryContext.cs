using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.BuildWarningSummary
{
    public class BuildWarningSummaryQueryContext : IQueryContext
    {
        public BuildWarningSummaryQueryContext(
            IBuildService builds,
            IWarningService warnings)
        {
            Builds = builds;
            Warnings = warnings;
        }

        public IBuildService Builds { get; }

        public IWarningService Warnings { get; }
    }
}
