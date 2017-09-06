using System;
using System.Collections.Generic;
using System.Linq;
using Warner.Domain;

namespace Warner.Reportage.ViewModels
{
    public class WarningOfTypeForBuildDetailsViewModel
    {
        private IEnumerable<BuildWarning> warnings;

        public WarningOfTypeForBuildDetailsViewModel(
            IEnumerable<BuildWarning> warnings)
        {
            this.warnings = warnings;
        }

        public string WarningType
        {
            get
            {
                if (Warnings.Count == 0)
                {
                    return "n/a";
                }
                return Warnings.First().WarningType;
            }
        }

        public List<BuildWarning> Warnings
        {
            get
            {
                return warnings.ToList();
            }
        }
    }
}
