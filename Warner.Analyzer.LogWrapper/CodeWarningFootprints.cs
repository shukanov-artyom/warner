using System;
using System.Collections.Generic;
using System.Linq;
using Warner.Analyzer.LogWrapper.WarnTypeParsers;

namespace Warner.Analyzer.LogWrapper
{
    internal static class CodeWarningFootprints
    {
        private static readonly IDictionary<string, WarningType> footprints =
            new Dictionary<string, WarningType>()
            {
                { "warning : sa", WarningType.SA },
                {  "warning ca", WarningType.CA },
                {  "warning cs", WarningType.CS }
            };

        public static List<string> All => footprints.Keys.ToList();

        public static WarningType Classify(string line)
        {
            line = line.ToLower();
            foreach (var footprint in footprints)
            {
                if (line.Contains(footprint.Key))
                {
                    return footprint.Value;
                }
            }
            throw new NotSupportedException(
                $"Could not determine warning type of this log line: {line}");
        }
    }
}
