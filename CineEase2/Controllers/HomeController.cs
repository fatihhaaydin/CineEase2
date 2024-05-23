using CineEase2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CineEase2.Controllers
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

        public IActionResult Details(int photoId)
        {
            ViewData["PhotoId"] = photoId;
            return View("~/Views/Home/Details.cshtml");
        }

        public IActionResult Buy(int photoId)
        {
            ViewData["PhotoId"] = photoId;
            return View("~/Views/Home/Buy.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
