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

        public async Task<IActionResult> Index()
        {
            var vehicles = await WebService.FetchVehicles();
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehicle vehicle) //submit button pressed
        {
            if (ModelState.IsValid)
            {
                await WebService.AddVehicle(vehicle);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var vehicles = await WebService.FetchVehicles();
            var vehicle = vehicles.FirstOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicles = await WebService.FetchVehicles();
            var vehicle = vehicles.Find(v => v.VehicleId == id);

            if (vehicle == null)
                return NotFound();

            await WebService.RemoveVehicle(vehicle);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var vehicles = await WebService.FetchVehicles();
            var vehicle = vehicles.FirstOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirmed(int id, Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                await WebService.UpdateVehicle(vehicle);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicles = await WebService.FetchVehicles();
            var vehicle = vehicles.Find(v => v.VehicleId == id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }
    }
}
