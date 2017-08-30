using System;

namespace Warner.Analyzer.Repository
{
    public class SvnBlameEntry
    {
        public int Revision { get; set; }

        public string DeveloperName { get; set; }

        public DateTimeOffset ModificationDate { get; set; }

        // will not be used to save memory
        public string LineContent { get; set; }
    }
}
