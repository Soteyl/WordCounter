using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using WordCounter.Core;
using WordCounter.Models;

namespace WordCounter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PhraseDensityByUrlService _densityService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _densityService = new PhraseDensityByUrlService();
        }

        public IActionResult Index()
        {
            return View(_densityService.Initialize());
        }

        [HttpPost("/")]
        public IActionResult AnalizePage(PhraseDensitiesModel model)
        {
            return View("index", _densityService.AnalizePage(model));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}