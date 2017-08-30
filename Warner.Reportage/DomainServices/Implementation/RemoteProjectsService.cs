using System;
using System.Collections.Generic;
using Warner.Api.Gateway;
using Warner.Domain;

namespace Warner.Reportage.DomainServices.Implementation
{
    public class RemoteProjectsService : IProjectsService
    {
        private readonly IWarnerReportingService webApi;

        public RemoteProjectsService(IWarnerReportingService webApi)
        {
            this.webApi = webApi
                ?? throw new ArgumentNullException(nameof(webApi));
        }

        public IEnumerable<Project> GetAll()
        {
            return webApi.GetAllProjects();
        }
    }
}
