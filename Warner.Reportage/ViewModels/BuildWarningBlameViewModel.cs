using System;
using System.Collections.Generic;

namespace Warner.Reportage.ViewModels
{
    public class BuildWarningBlameViewModel
    {
        public int Movement { get; set; }

        public List<BlameViewModel> Appeared { get; set; }

        public List<BlameViewModel> Disappeared { get; set; }
    }
}
