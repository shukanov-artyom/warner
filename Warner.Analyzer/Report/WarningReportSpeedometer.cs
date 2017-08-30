using System;

namespace Warner.Analyzer.Report
{
    internal class WarningReportSpeedometer
    {
        private readonly DateTime startTime;

        private int processedCount = 0;

        public WarningReportSpeedometer()
        {
            startTime = DateTime.Now;
        }

        public void IncrementBy(int warningsCount)
        {
            processedCount += warningsCount;
            Console.WriteLine($"Reported {warningsCount} warnings, total {processedCount}.");
            TimeSpan timeElapsed = DateTime.Now - startTime;
            double seconds = timeElapsed.TotalSeconds;
            double speed = processedCount / seconds;
            Console.WriteLine($"Avg. speed: {speed:0.##} warns/sec");
        }
    }
}
