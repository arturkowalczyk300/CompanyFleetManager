using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerWebApp.Services;
using CompanyFleetManagerWebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CompanyFleetManagerWebApp.Controllers
{
    public class VehiclesController : Controller
    {
        WebServiceFleetApi WebService;

        public VehiclesController(WebServiceFleetApi webService)
        {
            WebService = webService;
        }

        public IActionResult Index()
        {
            var vehicles = WebService.FetchVehicles();
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
                WebService.AddVehicle(vehicle);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var vehicle = WebService.FetchVehicles().FirstOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var vehicle = WebService.FetchVehicles.Find(id);

            if (vehicle == null)
                return NotFound();

            WebService.RemoveVehicle(vehicle);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var vehicle = WebService.FetchVehicles().FirstOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirmed(int id, Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                WebService.UpdateVehicle(vehicle);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        public IActionResult Details(int id)
        {
            var vehicle = WebService.FetchVehicles().Find(id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }
    }
}
