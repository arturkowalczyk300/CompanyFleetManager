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
        public async Task<IActionResult> Index()
        {
            var employees = await WebService.FetchEmployees();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await WebService.AddEmployee(employee);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var employees = await WebService.FetchEmployees();
            var employee = employees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await WebService.FetchEmployees();
            var employee = employees.Find(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            await WebService.RemoveEmployee(employee);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var employees = await WebService.FetchEmployees();
            var employee =  employees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirmed(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                await WebService.UpdateEmployee(employee);

                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel() { DetailedMessage = $"Model state is not valid! Following entries are invalid: {Utils.GetNamesOfNonValidEntries(ModelState)}" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var employees = await WebService.FetchEmployees();
            var employee = employees.Find(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

    }
}
