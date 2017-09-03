using System;
using System.Collections.Generic;
using Warner.Domain;

namespace Warner.Reportage.DomainServices
{
    public interface IBuildsService
    {
        Build Get(long buildId);

        List<Build> GetAllForProject(string projectName);

        List<Build> GetAllForProject(long projectId);
    }
}
