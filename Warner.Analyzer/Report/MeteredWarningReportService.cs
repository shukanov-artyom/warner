using System;
using Warner.Api.Gateway;
using Warner.Domain;

namespace Warner.Analyzer.Report
{
    public class MeteredWarningReportService : IWarningReportService
    {
        private const int BufferedWarningsCount = 50;

        private readonly IWarnerService service;
        private readonly Object locker = new object();
        private readonly WarningReportSpeedometer speedometer;

        private BuildWarning[] buffer;
        private int counter;

        public MeteredWarningReportService(IWarnerService service)
        {
            this.service = service ??
                throw new ArgumentNullException(nameof(service));
            buffer = new BuildWarning[BufferedWarningsCount];
            speedometer = new WarningReportSpeedometer();
        }

        public Project GetProject(string projectName)
        {
            return service.GetProject(projectName);
        }

        public Project SaveProject(Project project)
        {
            return service.SaveProject(project);
        }

        public Build AddBuild(Build newBuild)
        {
            return service.AddBuild(newBuild);
        }

        public void ReportWarning(BuildWarning warning)
        {
            lock (locker) // expect multiple threads wanting to do it
            {
                if (counter < buffer.Length)
                {
                    buffer[counter] = warning;
                    counter++;
                }
                else
                {
                    service.ReportWarningBatch(buffer);
                    buffer = new BuildWarning[BufferedWarningsCount];
                    speedometer.IncrementBy(BufferedWarningsCount);
                    counter = 0;
                }
            }
        }
    }
}
