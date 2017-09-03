using System;
using System.Collections.Generic;

namespace Warner.Domain.Surrogate
{
    /// <summary>
    /// Contains information about who created new warnings in build
    /// and who removed warnings in build.
    /// </summary>
    public class BuildBlameInfo
    {
        public BuildBlameInfo(
            IReadOnlyDictionary<string, WarningInBuildBlameInfo> blames)
        {
            Blames = blames;
        }

        // Key is WarningType, Value is warning blame info;
        public IReadOnlyDictionary<string, WarningInBuildBlameInfo> Blames { get; }
    }
}
