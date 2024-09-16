using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerWebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CompanyFleetManagerWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly FleetDatabaseContext DbContext;

        public EmployeesController(FleetDatabaseContext dbContext)
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

            return View("Error", new ErrorViewModel() { DetailedMessage = "Model state is not valid!" });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = DbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = DbContext.Employees.Find(id);

            if (employee == null)
                return NotFound();

            DbContext.Employees.Remove(employee);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = DbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirmed(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                DbContext.Employees.Update(employee);
                DbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = "Model state is not valid!"});
        }

        public IActionResult Details(int id)
        {
            var employee = DbContext.Employees.Find(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

    }
}
