using System;
using System.ComponentModel.DataAnnotations;

namespace Warner.Persistency.Entities
{
    public abstract class Entity
    {
        /// <summary>
        /// Surrogate primary key for all entitites.
        /// </summary>
        [Key]
        public long Id { get; set; }
    }
}
