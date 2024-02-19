using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebFootballers.AppServices;
using WebFootballers.Models;

namespace WebFootballers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FootballerService _footballerService;


        public HomeController(ILogger<HomeController> logger, FootballerService footballerService)
        {
            _logger = logger;
            _footballerService = footballerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FootballerList()
        {
            var footballers = _footballerService.GetAllFootballers();
            return View(footballers);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}