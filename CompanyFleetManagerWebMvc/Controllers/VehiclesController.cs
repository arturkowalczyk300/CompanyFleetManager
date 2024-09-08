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
    }
}
