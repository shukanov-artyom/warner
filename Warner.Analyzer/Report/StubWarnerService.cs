using System;
using Warner.Api.Gateway;
using Warner.Domain;

namespace Warner.Analyzer.Report
{
    public class StubWarnerService : IWarnerService
    {
        public Project GetProject(string projectName)
        {
            throw new NotImplementedException();
        }

        public Project SaveProject(Project project)
        {
            throw new NotImplementedException();
        }

        public Build AddBuild(Build newBuild)
        {
            throw new NotImplementedException();
        }

        public void ReportWarningBatch(BuildWarning[] warnings)
        {
            Console.WriteLine($"Service received {warnings.Length} warnings.");
        }
    }
}
