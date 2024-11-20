using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using CompanyFleetManagerWebApp.Services;
using CompanyFleetManagerWebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CompanyFleetManagerWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        WebServiceFleetApi WebService;

        public EmployeesController(WebServiceFleetApi webService)
        {
            WebService = webService;
        }
        public IActionResult Index()
        {
            var employees = WebService.FetchEmployees();
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
                WebService.AddEmployee(employee);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = WebService.FetchEmployees().FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = WebService.FetchEmployees()?.Find(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            WebService.RemoveEmployee(employee);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = WebService.FetchEmployees().FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirmed(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                WebService.UpdateEmployee(employee);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        public IActionResult Details(int id)
        {
            var employee = WebService.FetchEmployees()?.Find(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

    }
}
