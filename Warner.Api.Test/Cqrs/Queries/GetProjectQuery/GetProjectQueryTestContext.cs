using System;
using Warner.Api.Queries;
using Warner.Api.Test.Services;

namespace Warner.Api.Test.Cqrs.Queries.GetProjectQuery
{
    public class GetProjectQueryTestContext : GetProjectQueryContext
    {
        public GetProjectQueryTestContext()
            : base(new ProjectTestService())
        {
        }
    }
}
