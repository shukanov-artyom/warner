using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries
{
    public class GetProjectQueryResult : IQueryResult
    {
        public bool IsSuccess { get; set; }

        public Project Project { get; set; }
    }
}
