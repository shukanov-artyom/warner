using System;
using System.Collections.Generic;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries.GetAllBuildsForProject
{
    public class GetAllBuildsForProjectQuery : IQuery
    {
        private readonly string projectName;

        public GetAllBuildsForProjectQuery(string projectName)
        {
            this.projectName = projectName;
        }

        public IQueryResult Run(IQueryContext context)
        {
            GetAllBuildsForProjectQueryContext typedContext =
                context as GetAllBuildsForProjectQueryContext;
            List<Build> buildsForProject =
                typedContext.Builds.GetAllForProject(projectName);
            return new GetAllBuildsForProjectQueryResult(true, buildsForProject);
        }
    }
}
