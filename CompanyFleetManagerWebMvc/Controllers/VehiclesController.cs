using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFleetManagerWebApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly DatabaseContext DbContext;

        public VehiclesController(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public IActionResult Index()
        {
            var vehicles = DbContext.Vehicles.ToList();
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vehicle vehicle) //submit button pressed
        {
            if (ModelState.IsValid)
            {
                DbContext.Vehicles.Add(vehicle);
                DbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var vehicle = DbContext.Vehicles.FirstOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var vehicle = DbContext.Vehicles.Find(id);

            if (vehicle == null)
                return NotFound();

            DbContext.Vehicles.Remove(vehicle);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var vehicle = DbContext.Vehicles.FirstOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirmed(int id, Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                DbContext.Vehicles.Update(vehicle);
                DbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            var vehicle = DbContext.Vehicles.Find(id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }
    }
}
