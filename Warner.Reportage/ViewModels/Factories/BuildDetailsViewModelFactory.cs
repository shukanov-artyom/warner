using System;
using Warner.Domain;
using Warner.Reportage.DomainServices;

namespace Warner.Reportage.ViewModels.Factories
{
    public class BuildDetailsViewModelFactory
    {
        private readonly IBuildsService builds;
        private readonly IWarningService warnings;

        public BuildDetailsViewModelFactory(
            IBuildsService builds,
            IWarningService warnings)
        {
            this.builds = builds;
            this.warnings = warnings;
        }

        public BuildDetailsViewModel Create(long buildId)
        {
            Build build = builds.Get(buildId);
            var summary = warnings.GetSummaryForBuild(buildId);
            return new BuildDetailsViewModel(build, summary);
        }
    }
}
