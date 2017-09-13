using System;
using Warner.Domain;
using Warner.Reportage.ViewModels.Factories;

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

        public string TimeAmountSinceBuilt
        {
            get
            {
                var amount = new NiceTimeAmount(
                    Model.BuildDate,
                    DateTimeOffset.Now);
                return amount.GetDisplayValue();
            }
        }
    }
}
