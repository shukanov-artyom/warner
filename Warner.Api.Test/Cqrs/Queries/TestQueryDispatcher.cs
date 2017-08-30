using System;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Test.Cqrs.Queries.GetProjectQuery;

namespace Warner.Api.Test.Cqrs.Queries
{
    internal class TestQueryDispatcher
    {
        public IQueryResult Submit(IQuery query)
        {
            if (query is Api.Queries.GetProjectQuery)
            {
                var queryHandler = new GetProjectQueryTestHandler();
                return queryHandler.Handle(query as Api.Queries.GetProjectQuery);
            }
            throw new NotImplementedException("Query type is not supported yet.");
        }
    }
}
