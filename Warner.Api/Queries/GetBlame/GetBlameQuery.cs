using System;
using System.Threading.Tasks;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;
using Warner.Domain.Surrogate;

namespace Warner.Api.Queries.GetBlame
{
    public class GetBlameQuery : IQuery
    {
        private readonly long buildId;

        public GetBlameQuery(long buildId)
        {
            this.buildId = buildId;
        }

        public IQueryResult Run(IQueryContext context)
        {
            var typedContext = context as GetBlameQueryContext;
            IWarningService warnings = typedContext.Warnings;
            throw new NotImplementedException();
            //var payload = warnings.GetBlameForBuild(buildId);
            //return new GetBlameQueryResult(payload);
        }
    }

    public class GetBlameQueryResult : IQueryResult
    {
        public GetBlameQueryResult(BuildBlameInfo blame)
        {
            Blame = blame;
            IsSuccess = true;
        }

        public bool IsSuccess { get; }

        public BuildBlameInfo Blame { get;  }
    }

    public class GetBlameQueryHandler : IAsyncQueryHandler<GetBlameQuery>
    {
        private readonly IWarningService warnings;

        public GetBlameQueryHandler(IWarningService warnings)
        {
            this.warnings = warnings;
        }

        public Task<IQueryResult> GetColdTask(GetBlameQuery query)
        {
            var context = new GetBlameQueryContext(warnings);
            return new Task<IQueryResult>(() => query.Run(context));
        }
    }

    public class GetBlameQueryContext : IQueryContext
    {
        public GetBlameQueryContext(IWarningService warnings)
        {
            Warnings = warnings;
        }

        public IWarningService Warnings { get; }
    }
}
