using Calculator.Common.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
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

        public IActionResult History()
        {
            var history = HttpContext.Session.GetObjectFromJson<List<CalculationHistory>>("CalculationHistory") ?? new List<CalculationHistory>();
            return View(history); //Hesaplama geçmiþini alýp görünüme iletir
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }); //Hata sayfasýný gösterir
        }
    }
}
