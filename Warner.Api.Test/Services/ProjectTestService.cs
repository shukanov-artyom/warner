using System;
using System.Collections.Generic;
using System.Linq;
using Warner.Api.Services;
using Warner.Domain;

namespace Warner.Api.Test.Services
{
    public class ProjectTestService : IProjectService
    {
        private IList<Project> projects =
            new List<Project>();

        public Project GetByName(string name)
        {
            return projects.FirstOrDefault(p => p.Name == name);
        }

        public Project SaveNew(Project project)
        {
            projects.Add(project);
            return project;
        }

        public List<Project> GetAll()
        {
            return projects.ToList();
        }
    }
}
