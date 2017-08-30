using System;
using System.Threading.Tasks;

namespace Warner.Api.Cqrs.Infrastructure
{
    public interface IAsyncQueryHandler<in TQuery>
        where TQuery : IQuery
    {
        Task<IQueryResult> GetColdTask(TQuery query);
    }
}
