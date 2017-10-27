using System;
using Newtonsoft.Json;
using Warner.Api.Configuration;
using Warner.Domain;

namespace Warner.Api.Gateway
{
    /// <summary>
    /// Service for submitting data.
    /// </summary>
    public class WebApiWarnerService : WebServiceBase, IWarnerService
    {
        public WebApiWarnerService(WarnerApiConfiguration config)
            : base(config.ServiceUrl)
        {
        }

        public Project GetProject(string projectName)
        {
            return QueryParse<Project>($@"api\Project\{projectName}");
        }

        public Project SaveProject(Project project)
        {
            string stringPayload = JsonConvert.SerializeObject(project);
            Post(@"api\Project", stringPayload);
            return GetProject(project.Name);
        }

        public Build AddBuild(Build newBuild)
        {
            if (newBuild.ProjectId == 0)
            {
                throw new InvalidOperationException(
                    "Cannot accept build without valid project.");
            }
            string stringPayload = JsonConvert.SerializeObject(newBuild);
            long newBuildId = Post<long>(@"api\Build", stringPayload);
            return GetBuild(newBuildId);
        }

        public void ReportWarningBatch(BuildWarning[] warnings)
        {
            foreach (BuildWarning warning in warnings)
            {
                if (warning.BuildId == 0)
                {
                    throw new ArgumentException("warning.Build");
                }
                if (warning.DeveloperName == null)
                {
                    throw new ArgumentException("warning.Developer");
                }
            }
            string stringPayload = JsonConvert.SerializeObject(warnings);
            Post(@"api\Warning", stringPayload);
        }

        private Build GetBuild(long id)
        {
            if (id == 0)
            {
                throw new InvalidOperationException(
                    "Build id should be not 0 at this point.");
            }
            return QueryParse<Build>($@"api\Build\{id}");
        }
    }
}