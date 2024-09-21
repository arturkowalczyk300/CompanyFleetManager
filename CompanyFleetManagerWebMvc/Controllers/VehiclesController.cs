using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerWebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CompanyFleetManagerWebApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly FleetDatabaseContext DbContext;

        public VehiclesController(FleetDatabaseContext dbContext)
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

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
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

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
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
