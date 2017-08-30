using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Queries;
using Warner.Api.Services;

namespace Warner.Api.QueryHandlers
{
    public class GetProjectQueryHandler : IAsyncQueryHandler<GetProjectQuery>
    {
        private readonly IProjectService service;

        public GetProjectQueryHandler(IProjectService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public Task<IQueryResult> GetColdTask(GetProjectQuery query)
        {
            IQueryContext context = new GetProjectQueryContext(service);
            return new Task<IQueryResult>(() => query.Run(context));
        }
    }
}
