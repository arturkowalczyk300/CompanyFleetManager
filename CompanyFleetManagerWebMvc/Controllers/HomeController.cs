using CompanyFleetManager;
using CompanyFleetManagerWebApp;
using CompanyFleetManagerWebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CompanyFleetManagerWebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _dbContext;

        public HomeController(ILogger<HomeController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string text)
        {
            return View(new ErrorViewModel { DetailedMessage = text});
        }

        [ActionName("Utils")]
        public IActionResult UtilsSubpage()
        {
            return View();
        }

        public IActionResult Seed()
        {
            var result = Utils.SeedData(_dbContext);

            if (!result.Item1)
            {
                return View("Error", new ErrorViewModel() { DetailedMessage = "Seeding data failed!" });
            }

            return RedirectToAction("Index");
        }
    }
}
