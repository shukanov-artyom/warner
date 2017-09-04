using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            Build previousBuild = builds.GetAllForProject(build.ProjectId)
                .Where(b => b.Id != buildId && b.BuildNumber < build.BuildNumber)
                .OrderByDescending(b => b.BuildNumber)
                .FirstOrDefault();
            if (previousBuild == null)
            {
                throw new NotImplementedException(
                    "TODO: Process a case when we look at the first build.");
            }
            var currentSummary = warnings.GetSummaryForBuild(build.Id);
            var prevSummary = warnings.GetSummaryForBuild(previousBuild.Id);
            ReadOnlyDictionary<string, int> movements =
                new MovementFactory(currentSummary, prevSummary).Create();
            return new BuildDetailsViewModel()
            {
                BuildId = buildId,
                WarningsTotalCountCurrent = currentSummary.Sum(w => w.Value),
                WarningsTotalCountPrevious = prevSummary.Sum(w => w.Value),
                Summary = new ReadOnlyDictionary<string, int>(currentSummary),
                WarningMovements = movements,
            };
        }
    }
}
