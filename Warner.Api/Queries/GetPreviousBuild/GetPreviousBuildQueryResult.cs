using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries.GetPreviousBuild
{
    public class GetPreviousBuildQueryResult : IQueryResult
    {
        public GetPreviousBuildQueryResult(Build previousBuild)
        {
            PreviousBuild = previousBuild;
        }

        public bool IsSuccess { get; }

        public Build PreviousBuild { get; }
    }
}