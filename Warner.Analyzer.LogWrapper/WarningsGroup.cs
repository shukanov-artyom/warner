using System;
using System.Collections.Generic;
using Warner.Domain;

namespace Warner.Analyzer.Commands
{
    public class WarningsGroup
    {
        public WarningsGroup()
        {
            Warnings = new List<BuildWarning>();
        }

        public IList<BuildWarning> Warnings
        {
            get;
            private set;
        }

        public string SourceFileName
        {
            get;
            set;
        }
    }
}
