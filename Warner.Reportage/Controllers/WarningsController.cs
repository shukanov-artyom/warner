using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Warner.Domain;
using Warner.Reportage.DomainServices;
using Warner.Reportage.ViewModels;

namespace Warner.Reportage.Controllers
{
    public class WarningsController : Controller
    {
        private readonly IWarningService warningService;

        public WarningsController(IWarningService warningService)
        {
            this.warningService = warningService;
        }

        public IActionResult Stats(long buildId)
        {
            IEnumerable<BuildWarning> warnings =
                warningService.AllForBuild(buildId);
            var viewModel = new BuildStatsViewModel(warnings.ToList());
            return View(viewModel);
        }
    }
}
