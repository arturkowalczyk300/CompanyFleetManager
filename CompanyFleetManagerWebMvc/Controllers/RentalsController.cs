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
            return View();
        }

        public IActionResult IndexList()
        {
            var rentals = DbContext.Rentals.ToList();
            return View(rentals);
        }
    }
}
