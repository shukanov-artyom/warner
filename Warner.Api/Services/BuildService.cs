using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Warner.Domain;
using Warner.Persistency;
using Warner.Persistency.Entities;

namespace Warner.Api.Services
{
    public class BuildService : IBuildService
    {
        private readonly ApplicationDataContext dataContext;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BuildService(
            ApplicationDataContext dataContext,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.dataContext = dataContext
                ?? throw new ArgumentNullException(nameof(dataContext));
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public Build GetById(long id)
        {
            BuildEntity entity = dataContext.Builds.First(b => b.Id == id);
            return mapper.Map<Build>(entity);
        }

        public Build GetByIdentifierString(
            long projectId,
            long buildNumber)
        {
            BuildEntity entity = dataContext.Builds
                .Where(b => b.ProjectId == projectId)
                .FirstOrDefault(b => b.BuildNumber == buildNumber);
            return mapper.Map<Build>(entity);
        }

        public Build GetPreviousFor(long buildId)
        {
            BuildEntity currentBuildEntity = dataContext.Builds
                .First(b => b.Id == buildId);
            long projectId = currentBuildEntity.ProjectId;
            BuildEntity prevBuildEntity = dataContext.Builds
                .Where(b => b.ProjectId == projectId
                            && b.BuildNumber < currentBuildEntity.BuildNumber)
                .OrderByDescending(b => b.BuildNumber)
                .First();
            return Mapper.Map<Build>(prevBuildEntity);
        }

        public Build SaveNew(Build build)
        {
            BuildEntity buildEntity = mapper.Map<BuildEntity>(build);
            dataContext.Builds.Add(buildEntity);
            dataContext.SaveChanges();
            return GetByIdentifierString(build.ProjectId, build.BuildNumber);
        }

        public List<Build> GetAllForProject(string projectName)
        {
            // using local function here!
            IEnumerable<Build> GetAll(string projectNameUpper)
            {
                foreach (BuildEntity entity in
                    dataContext.Builds.Where(b => b.Project.Name == projectNameUpper))
                {
                    yield return mapper.Map<Build>(entity);
                }
            }
            return GetAll(projectName.ToUpper()).ToList();
        }

        public List<Build> GetAllForProject(long projectId)
        {
            IEnumerable<Build> GetAll(long projectIdLocal)
            {
                foreach (BuildEntity entity in
                    dataContext.Builds.Where(b => b.ProjectId == projectIdLocal))
                {
                    yield return mapper.Map<Build>(entity);
                }
            }
            return GetAll(projectId).ToList();
        }
    }
}
