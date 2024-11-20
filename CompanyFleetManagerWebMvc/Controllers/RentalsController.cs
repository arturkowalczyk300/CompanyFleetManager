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

        public IActionResult Index()
        {
            var rentals = WebService.FetchRentals();
            var employees = WebService.FetchEmployees();
            var vehicles = WebService.FetchVehicles();

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
        public IActionResult Create()
        {
            ViewBag.Employees = WebService.FetchEmployees();
            ViewBag.Vehicles = WebService.FetchVehicles();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Rental rental)
        {
            rental.RentedVehicle = WebService.FetchVehicles().FirstOrDefault(v => v.VehicleId == rental.RentedVehicleId);
            rental.RentingEmployee = WebService.FetchEmployees().FirstOrDefault(e => e.EmployeeId == rental.RentingEmployeeId);

            ModelState.Remove("RentedVehicle");
            ModelState.Remove("RentingEmployee");

            if (rental.RentedVehicle == null || rental.RentingEmployee == null)
            {
                return View("Error", new ErrorViewModel() { DetailedMessage = "Invalid Vehicle or Employee selection!" });
            }

            if (ModelState.IsValid)
            {
                WebService.AddRental(rental);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var rental = WebService.FetchRentals().FirstOrDefault(r => r.RentalId == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var rental = WebService.FetchRentals().Find(id);

            if (rental == null)
                return NotFound();

            WebService.RemoveRental(rental);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var rental = WebService.FetchRentals().FirstOrDefault(r => r.RentalId == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirmed(int id, Rental rental)
        {
            if (ModelState.IsValid)
            {
                WebService.UpdateRental(rental);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        public IActionResult Details(int id)
        {
            var rental = WebService.FetchRentals().Find(id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }
    }
}
