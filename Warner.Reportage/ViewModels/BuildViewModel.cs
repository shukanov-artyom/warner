using System;
using Warner.Domain;

namespace Warner.Reportage.ViewModels
{
    public class BuildViewModel
    {
        private readonly Build model;

        public BuildViewModel(Build model)
        {
            this.model = model ??
                throw new ArgumentNullException(nameof(model));
        }

        public string BuildNumberDisplay
        {
            get
            {
                return $"#{model.BuildNumber}";
            }
        }

        public DateTimeOffset BuildDateDisplay
        {
            get
            {
                return model.BuildDate;
            }
        }

        public Build Model => model;
    }
}
