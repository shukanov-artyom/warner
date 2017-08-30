using System;
using Warner.Api.Cqrs.Infrastructure;

namespace Warner.Api.Queries
{
    public class GetProjectQuery : IQuery
    {
        public GetProjectQuery(string projectName)
        {
            if (String.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            ProjectName = projectName;
        }

        private string ProjectName
        {
            get;
        }

        public IQueryResult Run(IQueryContext context)
        {
            var typedContext = context as GetProjectQueryContext;
            var project = typedContext.Projects.GetByName(ProjectName);
            return new GetProjectQueryResult()
            {
                Project = project,
                IsSuccess = project != null
            };
        }
    }
}
