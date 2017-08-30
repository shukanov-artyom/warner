using System;
using System.Threading.Tasks;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface IQueryDispatcher
    {
        Task<IQueryResult> Submit<TQuery>(TQuery query)
            where TQuery : IQuery;
    }
}
