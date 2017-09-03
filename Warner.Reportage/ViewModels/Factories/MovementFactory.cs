using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Warner.Reportage.ViewModels.Factories
{
    public class MovementFactory
    {
        private readonly IDictionary<string, int> current;
        private readonly IDictionary<string, int> previous;

        public MovementFactory(
            IDictionary<string, int> current,
            IDictionary<string, int> previous)
        {
            this.current = current;
            this.previous = previous;
        }

        public ReadOnlyDictionary<string, int> Create()
        {
            HashSet<string> allWarningCodes = new HashSet<string>();
            foreach (string code in current.Keys)
            {
                allWarningCodes.Add(code);
            }
            foreach (string code in previous.Keys)
            {
                allWarningCodes.Add(code);
            }
            var result = new Dictionary<string, int>();
            foreach (string code in allWarningCodes)
            {
                int currentCount = current.ContainsKey(code) ? current[code] : 0;
                int prevCount = previous.ContainsKey(code) ? previous[code] : 0;
                result[code] = currentCount - prevCount;
            }
            return new ReadOnlyDictionary<string, int>(result);
        }
    }
}
