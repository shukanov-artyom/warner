using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.GetPreviousBuild
{
    public class GetPreviousBuildQueryContext : IQueryContext
    {
        public GetPreviousBuildQueryContext(IBuildService builds)
        {
            Builds = builds;
        }

        public IBuildService Builds { get; }
    }
}