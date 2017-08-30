using System;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IQuery
    {
        public IQueryResult Run(IQueryContext context)
        {
            var typedContext = context as GetAllProjectsQueryContext;
            return new GetAllProjectsQueryResult(typedContext.Projects.GetAll());
        }
    }
}
