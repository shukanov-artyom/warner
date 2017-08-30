using System;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Test.Cqrs.Queries.GetProjectQuery
{
    public class GetProjectQueryTestHandler :
        ITestQueryHandler<Api.Queries.GetProjectQuery>
    {
        public IQueryResult Handle(Api.Queries.GetProjectQuery query)
        {
            var context = new GetProjectQueryTestContext();
            return query.Run(context);
        }
    }
}
