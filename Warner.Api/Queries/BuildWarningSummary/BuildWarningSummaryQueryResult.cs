using System;
using System.Collections.Generic;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Queries.BuildWarningSummary
{
    public class BuildWarningSummaryQueryResult : IQueryResult
    {
        public BuildWarningSummaryQueryResult(
            IDictionary<string, int> summary)
        {
            IsSuccess = true;
            Dictionary = summary;
        }

        public bool IsSuccess { get; }

        public IDictionary<string, int> Dictionary { get; }
    }
}
