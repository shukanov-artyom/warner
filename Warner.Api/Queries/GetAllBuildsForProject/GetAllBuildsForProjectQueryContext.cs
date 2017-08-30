using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.GetAllBuildsForProject
{
    public class GetAllBuildsForProjectQueryContext : IQueryContext
    {
        private readonly IBuildService buildService;

        public GetAllBuildsForProjectQueryContext(
            IBuildService buildService)
        {
            this.buildService = buildService;
        }

        public IBuildService Builds => buildService;
    }
}
