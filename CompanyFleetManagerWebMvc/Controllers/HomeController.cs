using CompanyFleetManager;
using CompanyFleetManagerWebApp;
using CompanyFleetManagerWebMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CompanyFleetManagerWebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FleetDatabaseContext _dbContext;

        public HomeController(ILogger<HomeController> logger, FleetDatabaseContext dbContext)
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

        [Authorize(Roles = "Admin")]
        public IActionResult Seed()
        {
            var error = Utils.SeedData(_dbContext);

            if (error is not null)
            {
                return View("Error", new ErrorViewModel() { DetailedMessage = $"Seeding data failed! Reason: {error.Message}" });
            }

            return RedirectToAction("Index");
        }
    }
}
