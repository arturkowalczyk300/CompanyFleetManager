using CompanyFleetManager.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyFleetManager.Controllers
{
    [ApiController]
    [Route("api/fleet")]
    public class FleetController : ControllerBase
    {
        private readonly FleetDatabaseAccess _dbAccess;

        public FleetController(FleetDatabaseAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        [HttpPost("vehicles")]
        public IActionResult AddVehicle(Vehicle vehicle)
        {
            _dbAccess.AddVehicle(vehicle);
            return CreatedAtAction(nameof(GetVehicles), new { id = vehicle.VehicleId }, vehicle);
        }

        [HttpPut("vehicles")]
        public IActionResult UpdateVehicle(Vehicle vehicle)
        {
            _dbAccess.UpdateVehicle(vehicle);
            return NoContent();
        }

        [HttpPost("employees")]
        public IActionResult AddEmployee(Employee employee)
        {
            _dbAccess.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployees), new { id = employee.EmployeeId }, employee);
        }

        [HttpPut("employees")]
        public IActionResult UpdateEmployee(Employee employee)
        {
            _dbAccess.UpdateEmployee(employee);
            return NoContent();
        }

        [HttpPost("rentals")]
        public IActionResult AddRental(Rental rental)
        {
            _dbAccess.AddRental(rental);
            return CreatedAtAction(nameof(GetRentals), new { id = rental.RentalId }, rental);
        }

        [HttpPut("rentals")]
        public IActionResult UpdateRental(Rental rental)
        {
            _dbAccess.UpdateRental(rental);
            return NoContent();
        }

        [HttpGet("vehicles")]
        public ActionResult<List<Vehicle>> GetVehicles()
        {
            return Ok(_dbAccess.GetVehicles());
        }

        [HttpGet("employees")]
        public ActionResult<List<Employee>> GetEmployees()
        {
            return Ok(_dbAccess.GetEmployees());
        }

        [HttpGet("rentals")]
        public ActionResult<List<Rental>> GetRentals()
        {
            return Ok(_dbAccess.GetRentals());
        }

        [HttpDelete("vehicles/{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = _dbAccess.GetVehicles().Find(v => v.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _dbAccess.DeleteVehicle(vehicle);
            return NoContent();
        }

        [HttpDelete("vehicles")]
        public IActionResult DeleteAllVehicles()
        {
            _dbAccess.DeleteAllVehicles();
            return NoContent();
        }

        [HttpDelete("employees")]
        public IActionResult DeleteAllEmployees()
        {
            _dbAccess.DeleteAllEmployees();
            return NoContent();
        }

        [HttpDelete("employees/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _dbAccess.GetEmployees().Find(e => e.EmployeeId == id);

            if(employee == null)
            {
                return NotFound();
            }

            _dbAccess.DeleteEmployee(employee);
            return NoContent();
        }

        [HttpDelete("rentals")]
        public IActionResult DeleteAllRentals()
        {
            _dbAccess.DeleteAllRentals();
            return NoContent();
        }


        [HttpDelete("rentals/{id}")]
        public IActionResult DeleteRental(int id)
        {
            var rental = _dbAccess.GetRentals().Find(r => r.RentalId == id);

            if (rental == null)
            {
                return NotFound();
            }

            _dbAccess.DeleteRentals(rental);
            return NoContent();
        }
    }
}
