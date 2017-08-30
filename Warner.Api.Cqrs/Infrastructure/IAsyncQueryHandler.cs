using System;
using System.Threading.Tasks;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface IAsyncQueryHandler<in TQuery>
        where TQuery : IQuery
    {
        IQueryResult RunQueryAsync(TQuery query);

        Task<IQueryResult> GetColdTask(TQuery query);
    }
}
