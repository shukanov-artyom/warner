using System;
using System.Collections.Generic;
using System.Linq;
using Warner.Domain;

namespace Warner.Reportage.ViewModels
{
    public class BuildStatsViewModel
    {
        private const int TopBottomCount = 10;

        private readonly List<BuildWarning> warnings;

        public BuildStatsViewModel(List<BuildWarning> warnings)
        {
            this.warnings = warnings;
        }

        public int TotalWarningsCount => warnings.Count;

        public IDictionary<string, int> TopWarnings
        {
            get
            {
                return warnings
                    .GroupBy(w => w.WarningType)
                    .OrderByDescending(g => g.Count())
                    .Take(TopBottomCount)
                    .ToDictionary(k => k.Key, v => v.Count());
            }
        }

        public IDictionary<string, int> BottomWarnings
        {
            get
            {
                return warnings
                    .GroupBy(w => w.WarningType)
                    .OrderBy(g => g.Count())
                    .Take(TopBottomCount)
                    .ToDictionary(k => k.Key, v => v.Count());
            }
        }
    }
}
