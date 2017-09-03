using System;
using System.Collections.Generic;
using Warner.Domain;

namespace Warner.Api.Gateway
{
    /// <summary>
    /// Used to retrieve data to build visual reports on warnings.
    /// </summary>
    public interface IWarnerReportingService
    {
        Build GetBuild(long id);

        IEnumerable<Project> GetAllProjects();

        IEnumerable<Build> GetAllBuildsForProject(string projectName);

        IEnumerable<Build> GetAllBuildsForProject(long projectId);

        IEnumerable<BuildWarning> GetAllWarningsForBuild(long buildId);

        IDictionary<string, int> GetSummaryForBuild(long buildId);

        IDictionary<string, int> GetMovementsForBuild(long buildId);
    }
}
