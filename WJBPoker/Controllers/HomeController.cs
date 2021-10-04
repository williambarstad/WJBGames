using WJBPoker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AspNetCoreWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Gameboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult Index()
        //{
        //    ViewData["Message"] = "You just created a ASP.Net Core web application!";
        //    return View();
        //}

        //public IActionResult Error()
        //{
        //    ViewData["Message"] = "We've encountered an error :(";
        //    return View();
        //}
    }
}
