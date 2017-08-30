using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warner.Persistency.Entities
{
    public class BuildWarningEntity : Entity
    {
        [Required]
        public string WarningType { get; set; }

        [Required]
        public string SourceFileName { get; set; }

        [Required]
        public int CodeLineNumber { get; set; }

        public string DeveloperName { get; set; }

        [ForeignKey("Build")]
        public long BuildId { get; set; }

        public BuildEntity Build { get; set; }

        [Required]
        public int LogLineNumber { get; set; }
    }
}
