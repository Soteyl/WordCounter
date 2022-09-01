using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using WordCounter.Core;
using WordCounter.Core.Interfaces;
using WordCounter.Models;

namespace WordCounter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IPhraseDensityByUrlService _densityService;

        public HomeController(ILogger<HomeController> logger, IPhraseDensityByUrlService densityService)
        {
            _logger = logger;
            _densityService = densityService;
        }

        public IActionResult Index()
        {
            return View(new PhraseDensitiesModel());
        }

        [HttpPost("/")]
        public IActionResult AnalyzePage(PhraseDensitiesModel model)
        {
            model.CurrentDensities = _densityService.AnalyzePage(model);
            return View("index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}