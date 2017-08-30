using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warner.Persistency.Entities
{
    public class BuildEntity : Entity
    {
        [ForeignKey("Project")]
        public long ProjectId { get; set; }

        public DateTimeOffset BuildDate { get; set; }

        public long BuildNumber { get; set; }

        public string LogFileName { get; set; }

        public ProjectEntity Project { get; set; }

        public List<BuildWarningEntity> BuildWarnings { get; set; }
    }
}
