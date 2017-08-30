using System;
using System.Collections.Generic;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries.GetAllBuildsForProject
{
    public class GetAllBuildsForProjectQueryResult : IQueryResult
    {
        public GetAllBuildsForProjectQueryResult(
            bool isSuccess,
            List<Build> builds)
        {
            IsSuccess = isSuccess;
            Builds = builds;
        }

        public List<Build> Builds { get; }

        public bool IsSuccess { get; }
    }
}
