using System;
using System.Collections.Generic;
using System.Linq;
using Warner.Domain;

namespace Warner.Reportage.ViewModels
{
    public class BuildDetailsViewModel
    {
        private readonly Build build;

        public BuildDetailsViewModel(
            Build build,
            IDictionary<string, int> warningsWithCounts,
            IDictionary<string, int> warningsWithMovements)
        {
            this.build = build;
            WarningsWithCounts = new List<WarningWithCountViewModel>();
            WarningsWithMovements = new WarningsMovementsViewModel(warningsWithMovements);
            foreach (KeyValuePair<string, int> pair in warningsWithCounts)
            {
                WarningsWithCounts.Add(
                    new WarningWithCountViewModel(pair.Key, pair.Value));
            }
        }

        public long BuildNumber
        {
            get
            {
                return build.BuildNumber;
            }
        }

        public int WarningsTotalCount
        {
            get
            {
                return WarningsWithCounts.Sum(wc => wc.WarningsCountInBuild);
            }
        }

        public List<WarningWithCountViewModel> WarningsWithCounts { get; }

        public WarningsMovementsViewModel WarningsWithMovements { get; }
    }
}
