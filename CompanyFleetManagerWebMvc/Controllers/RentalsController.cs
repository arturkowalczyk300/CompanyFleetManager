using CompanyFleetManager;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFleetManagerWebApp.Controllers
{
    public class RentalsController : Controller
    {
        private readonly DatabaseContext DbContext;

        public RentalsController(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public IActionResult Index()
        {
            var rentals = DbContext.Rentals.ToList();
            return View(rentals);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
