using System;
using System.Collections.ObjectModel;

namespace Warner.Reportage.ViewModels
{
    public class BuildDetailsViewModel
    {
        public int WarningsTotalCountCurrent { get; set; }

        public int WarningsTotalCountPrevious { get; set; }

        public ReadOnlyDictionary<string, int> WarningMovements { get; set; }

        public ReadOnlyDictionary<string, BuildWarningBlameViewModel> Blames { get; set; }
    }
}
