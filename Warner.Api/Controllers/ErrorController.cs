using System;
using Microsoft.AspNetCore.Mvc;

namespace Warner.Api.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error")]
        public IActionResult Index()
        {
            throw new Exception();
        }
    }
}
