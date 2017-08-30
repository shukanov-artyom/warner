using System;
using System.Collections.Generic;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries.GetAllWarningsForBuild
{
    public class GetAllWarningsForBuildQueryResult : IQueryResult
    {
        private readonly IEnumerable<BuildWarning> warnings;

        public GetAllWarningsForBuildQueryResult(
            IEnumerable<BuildWarning> warnings)
        {
            this.warnings = warnings;
        }

        public IEnumerable<BuildWarning> Warnings => warnings;

        public bool IsSuccess { get; }
    }
}
