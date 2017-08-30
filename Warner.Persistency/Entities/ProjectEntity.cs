using System;
using System.Collections.Generic;

namespace Warner.Persistency.Entities
{
    public class ProjectEntity : Entity
    {
        public string Name { get; set; }

        public List<BuildEntity> Builds { get; set; }
    }
}
