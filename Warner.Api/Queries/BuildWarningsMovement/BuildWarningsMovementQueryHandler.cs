using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Queries.BuildWarningsMovement
{
    public class BuildWarningsMovementQueryHandler :
        IAsyncQueryHandler<BuildWarningsMovementQuery>
    {
        public Task<IQueryResult> GetColdTask(BuildWarningsMovementQuery query)
        {
            var context = new BuildWarningsMovementQueryContext();
            return new Task<IQueryResult>(() => query.Run(context));
        }
    }
}