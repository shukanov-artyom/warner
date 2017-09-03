using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Warner.Domain;

namespace Warner.Reportage.ViewModels.Factories
{
    public class BuildWarningBlameFactory
    {
        private readonly ReadOnlyDictionary<string, int> movements;
        private readonly List<BuildWarning> current;
        private readonly List<BuildWarning> previous;

        public BuildWarningBlameFactory(
            ReadOnlyDictionary<string, int> movements,
            List<BuildWarning> current,
            List<BuildWarning> previous)
        {
            this.movements = movements;
            this.current = current;
            this.previous = previous;
        }

        public ReadOnlyDictionary<string, BuildWarningBlameViewModel> Create()
        {
            var result = new Dictionary<string, BuildWarningBlameViewModel>();
            foreach (string warn in movements.Keys)
            {
                if (movements[warn] == 0)
                {
                    continue;
                }
                var item = new BuildWarningBlameViewModel();
                item.Movement = movements[warn];
                item.Appeared = GetAppeared(warn).ToList();
                item.Disappeared = GetDisappeared(warn).ToList();
                result[warn] = item;
            }
            return new ReadOnlyDictionary<string, BuildWarningBlameViewModel>(result);
        }

        private IEnumerable<BlameViewModel> GetAppeared(string warn)
        {
            IEnumerable<BuildWarning> existing =
                previous.Where(w => w.WarningType == warn);
            foreach (BuildWarning warning in current.Where(w => w.WarningType == warn))
            {
                if (!existing.Any(w => w.CodeLineNumber == warning.CodeLineNumber
                    && w.SourceFileName == warning.SourceFileName))
                {
                    yield return new BlameViewModel()
                    {
                        CodeFile = warning.SourceFileName,
                        LineNumber = warning.CodeLineNumber,
                        Developer = warning.DeveloperName
                    };
                }
            }
        }

        private IEnumerable<BlameViewModel> GetDisappeared(string warn)
        {
            IEnumerable<BuildWarning> latest =
                current.Where(w => w.WarningType == warn);
            foreach (BuildWarning warning in previous.Where(w => w.WarningType == warn))
            {
                if (!latest.Any(w => w.CodeLineNumber == warning.CodeLineNumber
                                       && w.SourceFileName == warning.SourceFileName))
                {
                    yield return new BlameViewModel()
                    {
                        CodeFile = warning.SourceFileName,
                        LineNumber = warning.CodeLineNumber,
                        Developer = warning.DeveloperName
                    };
                }
            }
        }
    }
}
