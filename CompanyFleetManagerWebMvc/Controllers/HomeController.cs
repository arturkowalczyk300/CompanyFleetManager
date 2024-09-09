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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ActionName("Utils")]
        public IActionResult UtilsSubpage()
        {
            return View();
        }

        public IActionResult Seed()
        {
            Utils.SeedData(_dbContext);

            return RedirectToAction("Index");
        }
    }
}
