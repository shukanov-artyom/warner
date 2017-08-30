using System;

namespace Warner.Domain
{
    public class Build : DomainObject
    {
        public Build(long projectId)
        {
            ProjectId = projectId;
        }

        public Project Project { get; set; }

        public long ProjectId { get; }

        public DateTimeOffset BuildDate { get; set; }

        public long BuildNumber { get; set; }

        public string LogFileName { get; set; }
    }
}
