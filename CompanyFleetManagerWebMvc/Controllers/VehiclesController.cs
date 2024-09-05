using CompanyFleetManager;
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
    }
}
