using System;

namespace Warner.Analyzer.LogWrapper
{
    public class LogEntryInfo
    {
        public string WarningType { get; set; }

        public string SourceFilePathName { get; set; }

        public int CodeLineNumber { get; set; }

        public DateTime LoggedDate { get; set; }
    }
}
