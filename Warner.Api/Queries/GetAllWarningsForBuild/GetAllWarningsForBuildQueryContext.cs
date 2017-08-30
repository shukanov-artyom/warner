using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.GetAllWarningsForBuild
{
    public class GetAllWarningsForBuildQueryContext : IQueryContext
    {
        private readonly IWarningService warningService;

        public GetAllWarningsForBuildQueryContext(
            IWarningService warningService)
        {
            this.warningService = warningService;
        }

        public IWarningService Warnings => warningService;
    }
}
