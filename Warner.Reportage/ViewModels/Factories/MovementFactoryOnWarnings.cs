using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Warner.Domain;

namespace Warner.Reportage.ViewModels.Factories
{
    public class MovementFactoryOnWarnings
    {
        private readonly List<BuildWarning> current;
        private readonly List<BuildWarning> previous;

        public MovementFactoryOnWarnings(
            List<BuildWarning> current,
            List<BuildWarning> previous)
        {
            this.current = current;
            this.previous = previous;
        }

        public ReadOnlyDictionary<string, int> Create()
        {
            HashSet<string> allWarns = new HashSet<string>();
            current.ForEach(p => allWarns.Add(p.WarningType));
            previous.ForEach(p => allWarns.Add(p.WarningType));
            foreach (string warn in allWarns)
            {
            }
            throw new NotImplementedException();
        }
    }
}
