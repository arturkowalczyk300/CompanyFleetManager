using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                DbContext.Rentals.Add(rental);
                DbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(rental);
        }
    }
}
