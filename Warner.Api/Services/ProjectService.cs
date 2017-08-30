using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Warner.Domain;
using System.Linq;
using AutoMapper;
using Warner.Persistency;
using Warner.Persistency.Entities;

namespace Warner.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDataContext dataContext;
        private readonly IMapper mapper;

        public ProjectService(
            DbContext dataContext,
            IMapper mapper)
        {
            this.dataContext = dataContext as ApplicationDataContext;
            this.mapper = mapper;
        }

        public Project GetByName(string name)
        {
            name = name.ToLower();
            ProjectEntity entity = dataContext.Projects
                .FirstOrDefault(p => p.Name == name);
            if (entity == null)
            {
                return null;
            }
            return mapper.Map<Project>(entity);
        }

        public Project SaveNew(Project project)
        {
            ProjectEntity entity =
                mapper.Map<ProjectEntity>(project);
            dataContext.Projects.Add(entity);
            return mapper.Map<Project>(entity);
        }

        public List<Project> GetAll()
        {
            return GetAllPrivate().ToList();
        }

        private IEnumerable<Project> GetAllPrivate()
        {
            foreach (var entity in dataContext.Projects)
            {
                yield return mapper.Map<Project>(entity);
            }
        }
    }
}
