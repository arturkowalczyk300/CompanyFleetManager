using CompanyFleetManager;
using CompanyFleetManagerWebApp;
using CompanyFleetManagerWebApp.Services;
using CompanyFleetManagerWebMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CompanyFleetManagerWebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebServiceFleetApi _webService;

        public HomeController(ILogger<HomeController> logger, WebServiceFleetApi webService)
        {
            _logger = logger;
            _webService = webService;
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
            throw new NotImplementedException();

            //var error = Utils.SeedData(_dbContext);
            var error = "Error";

            if (error is not null)
            {
                //return View("Error", new ErrorViewModel() { DetailedMessage = $"Seeding data failed! Reason: {error.Message}" });
            }

            return RedirectToAction("Index");
        }
    }
}
