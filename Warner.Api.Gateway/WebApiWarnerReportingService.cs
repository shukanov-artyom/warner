using System;
using System.Collections.Generic;
using Warner.Api.Configuration;
using Warner.Domain;

namespace Warner.Api.Gateway
{
    /// <summary>
    /// Note that this service is readonly.
    /// </summary>
    public class WebApiWarnerReportingService :
        WarnerServiceBase, IWarnerReportingService
    {
        private readonly WarnerApiConfiguration config;

        public WebApiWarnerReportingService(WarnerApiConfiguration config)
            : base(config)
        {
            this.config = config
                ?? throw new ArgumentNullException(nameof(config));
        }

        public Build GetBuild(long id)
        {
            return QueryParse<Build>($@"api/Build/{id}");
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return QueryParse<List<Project>>(@"api/Project/All");
        }

        public IEnumerable<Build> GetAllBuildsForProject(string projectName)
        {
            return QueryParse<List<Build>>($@"api/Build/All/{projectName}");
        }

        public IEnumerable<Build> GetAllBuildsForProject(long projectId)
        {
            return QueryParse<List<Build>>($@"api/Build/All/{projectId}");
        }

        public IEnumerable<BuildWarning> GetAllWarningsForBuild(long buildId)
        {
            return QueryParse<List<BuildWarning>>($@"api/Warning/All/{buildId}");
        }

        public IDictionary<string, int> GetSummaryForBuild(long buildId)
        {
            return QueryParse<Dictionary<string, int>>(
                $@"api/Warning/Summary/{buildId}");
        }

        public IDictionary<string, int> GetMovementsForBuild(long buildId)
        {
            return QueryParse<Dictionary<string, int>>(
                $@"api/Warning/Movement/{buildId}");
        }
    }
}
