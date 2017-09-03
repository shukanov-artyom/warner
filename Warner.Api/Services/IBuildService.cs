using System;
using System.Collections.Generic;
using Warner.Domain;

namespace Warner.Api.Services
{
    public interface IBuildService
    {
        Build GetById(long id);

        Build GetPreviousFor(long buildId);

        Build SaveNew(Build build);

        List<Build> GetAllForProject(string projectName);

        List<Build> GetAllForProject(long projectId);
    }
}
