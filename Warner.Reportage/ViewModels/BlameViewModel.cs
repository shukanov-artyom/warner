using System;

namespace Warner.Reportage.ViewModels
{
    public class BlameViewModel
    {
        public string CodeFile { get; set; }

        public int LineNumber { get; set; }

        public string Developer { get; set; }
    }
}
