using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                DbContext.Employees.Add(employee);
                DbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(employee);
        }
    }
}
