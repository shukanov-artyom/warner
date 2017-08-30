using System;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface IQuery
    {
        IQueryResult Run(IQueryContext context);
    }
}
