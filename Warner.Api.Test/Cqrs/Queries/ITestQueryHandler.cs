using System;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Test.Cqrs.Queries
{
    internal interface ITestQueryHandler<in TQuery>
        where TQuery : IQuery
    {
        IQueryResult Handle(TQuery query);
    }
}
