using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Warner.Reportage.DomainServices;
using Warner.Reportage.ViewModels;
using Warner.Reportage.ViewModels.Factories;

namespace Warner.Reportage.Controllers
{
    public class BuildsController : Controller
    {
        private readonly IBuildsService builds;
        private readonly IWarningService warnings;

        public BuildsController(
            IBuildsService builds,
            IWarningService warnings)
        {
            this.builds = builds;
            this.warnings = warnings;
        }

        public IActionResult Index(string projectName)
        {
            var model = builds
                .GetAllForProject(projectName)
                .Select(p => new BuildViewModel(p));
            // let's process only 7 latest builds
            var top7 = model.OrderByDescending(m => m.BuildDateDisplay).Take(7);
            return View(top7);
        }

        public IActionResult Details(int buildId)
        {
            var viewModel = new BuildDetailsViewModelFactory(builds, warnings)
                .Create(buildId);
            return View(viewModel);
        }
    }
}
