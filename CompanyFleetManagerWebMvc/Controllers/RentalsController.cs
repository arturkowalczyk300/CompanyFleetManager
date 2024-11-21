using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerWebApp.Services;
using CompanyFleetManagerWebApp.ViewModels;
using CompanyFleetManagerWebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CompanyFleetManagerWebApp.Controllers
{
    public class RentalsController : Controller
    {
        WebServiceFleetApi WebService;

        public RentalsController(WebServiceFleetApi webService)
        {
            WebService = webService;
        }

        public async Task<IActionResult> Index()
        {
            var rentals = await WebService.FetchRentals();
            var employees = await WebService.FetchEmployees();
            var vehicles = await WebService.FetchVehicles();

            List<RentalViewModel> rentalViewModels = new List<RentalViewModel>();
            foreach (var rental in rentals)
            {
                var employee = employees.Find(x => x.EmployeeId == rental.RentingEmployeeId);
                var vehicle = vehicles.Find(x => x.VehicleId == rental.RentedVehicleId);

                rentalViewModels.Add(new RentalViewModel(rental, new ShortenedEmployeeData(employee), new ShortenedVehicleData(vehicle)));
            }

            return View(rentalViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Employees = await WebService.FetchEmployees();
            ViewBag.Vehicles = await WebService.FetchVehicles();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rental rental)
        {
            rental.RentedVehicle = (await WebService.FetchVehicles()).FirstOrDefault(v => v.VehicleId == rental.RentedVehicleId);
            rental.RentingEmployee = (await WebService.FetchEmployees()).FirstOrDefault(e => e.EmployeeId == rental.RentingEmployeeId);

            ModelState.Remove("RentedVehicle");
            ModelState.Remove("RentingEmployee");

            if (rental.RentedVehicle == null || rental.RentingEmployee == null)
            {
                return View("Error", new ErrorViewModel() { DetailedMessage = "Invalid Vehicle or Employee selection!" });
            }

            if (ModelState.IsValid)
            {
                await WebService.AddRental(rental);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var rentals = await WebService.FetchRentals();
            var rental = rentals.FirstOrDefault(r => r.RentalId == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentals = await WebService.FetchRentals();
            var rental = rentals.Find(r => r.RentalId == id);

            if (rental == null)
                return NotFound();

            await WebService.RemoveRental(rental);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var rentals = await WebService.FetchRentals();
            var rental = rentals.FirstOrDefault(r => r.RentalId == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirmed(int id, Rental rental)
        {
            if (ModelState.IsValid)
            {
                await WebService.UpdateRental(rental);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var rentals = await WebService.FetchRentals();
            var rental = rentals.Find(r => r.RentalId == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }
    }
}
