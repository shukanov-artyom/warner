using System;
using Warner.Domain;

namespace Warner.Reportage.ViewModels
{
    public class ProjectViewModel
    {
        private readonly Project model;

        public ProjectViewModel(Project model)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public string Name
        {
            get
            {
                return model.Name;
            }
        }
    }
}
