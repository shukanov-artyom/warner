using System;
using System.Collections.Generic;

namespace Warner.Domain.Surrogate
{
    public class WarningInBuildBlameInfo
    {
        public WarningInBuildBlameInfo(
            string warningType,
            List<Blame> blamesAdded,
            List<Blame> blamesRemoved)
        {
            WarningType = warningType;
            BlamesAdded = blamesAdded;
            BlamesRemoved = blamesRemoved;
        }

        public string WarningType { get; }

        public List<Blame> BlamesAdded { get; }

        public List<Blame> BlamesRemoved { get; }
    }
}
