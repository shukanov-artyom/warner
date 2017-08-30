using System;
using System.Collections.Generic;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries.GetAllProjects
{
    public class GetAllProjectsQueryResult : IQueryResult
    {
        public GetAllProjectsQueryResult(List<Project> projects)
        {
            Projects = projects
                ?? throw new ArgumentNullException(nameof(projects));
        }

        public bool IsSuccess { get; }

        public List<Project> Projects { get; }
    }
}
