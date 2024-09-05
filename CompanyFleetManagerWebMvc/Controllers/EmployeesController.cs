using CompanyFleetManager;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFleetManagerWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DatabaseContext DbContext;

        public EmployeesController(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }
        public IActionResult Index()
        {
            var employees = DbContext.Employees.ToList();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
