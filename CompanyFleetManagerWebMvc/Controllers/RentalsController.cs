using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerWebMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFleetManagerWebApp.Controllers
{
    public class RentalsController : Controller
    {
        private readonly FleetDatabaseContext DbContext;

        public RentalsController(FleetDatabaseContext dbContext)
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

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var rental = DbContext.Rentals.FirstOrDefault(r => r.RentalId == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var rental = DbContext.Rentals.Find(id);

            if (rental == null)
                return NotFound();

            DbContext.Rentals.Remove(rental);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var rental = DbContext.Rentals.FirstOrDefault(r => r.RentalId== id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirmed(int id, Rental rental)
        {
            if (ModelState.IsValid)
            {
                DbContext.Rentals.Update(rental);
                DbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        public IActionResult Details(int id)
        {
            var rental = DbContext.Rentals.Find(id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }
    }
}
