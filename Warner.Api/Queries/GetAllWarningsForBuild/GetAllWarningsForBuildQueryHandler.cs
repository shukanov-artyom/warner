using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;

namespace Warner.Api.Queries.GetAllWarningsForBuild
{
    public class GetAllWarningsForBuildQueryHandler :
        IAsyncQueryHandler<GetAllWarningsForBuildQuery>
    {
        private readonly IWarningService warningService;

        public GetAllWarningsForBuildQueryHandler(IWarningService warningService)
        {
            this.warningService = warningService;
        }

        public Task<IQueryResult> GetColdTask(GetAllWarningsForBuildQuery query)
        {
            var context = new GetAllWarningsForBuildQueryContext(warningService);
            return new Task<IQueryResult>(() => query.Run(context));
        }
    }
}
