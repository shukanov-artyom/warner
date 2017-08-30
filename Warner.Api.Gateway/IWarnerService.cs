using System;
using Warner.Domain;

namespace Warner.Api.Gateway
{
    public interface IWarnerService
    {
        Project GetProject(string projectName);

        Project SaveProject(Project project);

        Build AddBuild(Build newBuild);

        void ReportWarningBatch(BuildWarning[] warnings);
    }
}
