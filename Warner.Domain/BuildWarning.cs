using System;

namespace Warner.Domain
{
    public class BuildWarning : DomainObject
    {
        public string WarningType { get; set; }

        public string SourceFileName { get; set; }

        public int CodeLineNumber { get; set; }

        public string DeveloperName { get; set; }

        public long BuildId { get; set; }

        public Build Build { get; set; }

        public int LogLineNumber { get; set; }
    }
}
