using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries.GetBuld
{
    public class GetBuildQueryResult : IQueryResult
    {
        public GetBuildQueryResult(Build build)
        {
            Build = build;
        }

        public Build Build
        {
            get;
        }

        public bool IsSuccess { get; set; }
    }
}