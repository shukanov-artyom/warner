using System;
using System.Collections.Generic;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries.GetAllBuildsForProject
{
    public class GetAllBuildsForProjectQuery : IQuery
    {
        private readonly string projectName;
        private readonly long projectId;

        public GetAllBuildsForProjectQuery(string projectName)
        {
            this.projectName = projectName;
        }

        public GetAllBuildsForProjectQuery(long projectId)
        {
            this.projectId = projectId;
        }

        public IQueryResult Run(IQueryContext context)
        {
            GetAllBuildsForProjectQueryContext typedContext =
                context as GetAllBuildsForProjectQueryContext;
            List<Build> buildsForProject;
            if (!String.IsNullOrEmpty(projectName))
            {
                buildsForProject = typedContext.Builds.GetAllForProject(projectName);
            }
            else if (projectId != 0)
            {
                buildsForProject = typedContext.Builds.GetAllForProject(projectId);
            }
            else
            {
                throw new InvalidOperationException("Project name or id must be present.");
            }
            return new GetAllBuildsForProjectQueryResult(true, buildsForProject);
        }
    }
}
