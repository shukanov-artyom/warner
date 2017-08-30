using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.BuildWarningSummary
{
    public class BuildWarningSummaryQueryHandler :
        IAsyncQueryHandler<BuildWarningSummaryQuery>
    {
        private readonly IWarningService warnings;
        private readonly IBuildService builds;

        public BuildWarningSummaryQueryHandler(
            IBuildService builds,
            IWarningService warnings)
        {
            this.warnings = warnings;
            this.builds = builds;
        }

        public Task<IQueryResult> GetColdTask(BuildWarningSummaryQuery query)
        {
            var context = new BuildWarningSummaryQueryContext(builds, warnings);
            return new Task<IQueryResult>(() => query.Run(context));
        }
    }
}
