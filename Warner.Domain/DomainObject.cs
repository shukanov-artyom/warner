using System;

namespace Warner.Domain
{
    /// <summary>
    /// Unfortunately, my domain objects are also persistency objects.
    /// </summary>
    public abstract class DomainObject
    {
        public long Id { get; set; }

        public bool IsNew => Id == 0;
    }
}
