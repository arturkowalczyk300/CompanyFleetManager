using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerWebApp.ViewModels;
using CompanyFleetManagerWebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            var employees = DbContext.Employees.ToList();
            var vehicles = DbContext.Vehicles.ToList();

            List<RentalViewModel> rentalViewModels = new List<RentalViewModel>();
            foreach (var rental in rentals)
            {
                var employee = employees.Find(x=> x.EmployeeId == rental.RentingEmployeeId);
                var vehicle = vehicles.Find(x=>x.VehicleId == rental.RentedVehicleId);

                rentalViewModels.Add(new RentalViewModel(rental, new ShortenedEmployeeData(employee), new ShortenedVehicleData(vehicle)));
            }

            return View(rentalViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employees = DbContext.Employees.ToList();
            ViewBag.Vehicles = DbContext.Vehicles.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Rental rental)
        {
            rental.RentedVehicle = DbContext.Vehicles.FirstOrDefault(v => v.VehicleId == rental.RentedVehicleId);
            rental.RentingEmployee = DbContext.Employees.FirstOrDefault(e => e.EmployeeId == rental.RentingEmployeeId);

            ModelState.Remove("RentedVehicle");
            ModelState.Remove("RentingEmployee");

            if (rental.RentedVehicle == null || rental.RentingEmployee == null)
            {
                return View("Error", new ErrorViewModel() {DetailedMessage = "Invalid Vehicle or Employee selection!" });
            }

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

            var rental = DbContext.Rentals.FirstOrDefault(r => r.RentalId == id);

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
