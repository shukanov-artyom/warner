using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Warner.Reportage.DomainServices;
using Warner.Reportage.ViewModels;

namespace Warner.Reportage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectsService projectsService;

        public HomeController(
            IProjectsService projectsService)
        {
            this.projectsService = projectsService
                ?? throw new ArgumentNullException(nameof(projectsService));
        }

        public IActionResult Index()
        {
            List<ProjectViewModel> model = new List<ProjectViewModel>();
            foreach (var proj in projectsService.GetAll())
            {
                model.Add(new ProjectViewModel(proj));
            }
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
