using System;
using Warner.Domain;

namespace Warner.Analyzer.Report
{
    public interface IWarningReportService
    {
        Project GetProject(string projectName);

        Project SaveProject(Project project);

        Build AddBuild(Build newBuild);

        void ReportWarning(BuildWarning warning);

        void Finish();
    }
}
