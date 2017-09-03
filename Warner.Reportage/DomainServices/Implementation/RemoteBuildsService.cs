using System;
using System.Collections.Generic;
using System.Linq;
using Warner.Api.Gateway;
using Warner.Domain;

namespace Warner.Reportage.DomainServices.Implementation
{
    public class RemoteBuildsService : IBuildsService
    {
        private readonly IWarnerReportingService webApi;

        public RemoteBuildsService(IWarnerReportingService webApi)
        {
            this.webApi = webApi;
        }

        public Build Get(long id)
        {
            return webApi.GetBuild(id);
        }

        public List<Build> GetAllForProject(string projectName)
        {
            return webApi.GetAllBuildsForProject(projectName).ToList();
        }

        public List<Build> GetAllForProject(long projectId)
        {
            return webApi.GetAllBuildsForProject(projectId).ToList();
        }
    }
}
