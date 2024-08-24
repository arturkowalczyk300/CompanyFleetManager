using CompanyFleetManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFleetManager
{
    public class DatabaseAccess
    {
        private DatabaseContext DbContext;

        public DatabaseAccess(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            DbContext.Vehicles.Add(vehicle);
            DbContext.SaveChanges();
        }

        public void AddEmployee(Employee employee)
        {
            DbContext.Employees.Add(employee);
            DbContext.SaveChanges();
        }

        public void AddRental(Rental rental)
        {
            DbContext.Rentals.Add(rental);
            DbContext.SaveChanges();
        }

        public List<Vehicle> GetVehicles() => DbContext.Vehicles.ToList();

        public List<Employee> GetEmployees() => DbContext.Employees.ToList();

        public List<Rental> GetRentals() => DbContext.Rentals.ToList();

        public void DeleteVehicle(Vehicle vehicle)
        {
            DbContext.Vehicles.Remove(vehicle);
            DbContext.SaveChanges();
        }

        public void DeleteAllVehicles()
        {
            var all = from vehicles in DbContext.Vehicles select vehicles;
            DbContext.Vehicles.RemoveRange(all);
            DbContext.SaveChanges();
        }

        public void DeleteAllEmployees()
        {
            var all = from employees in DbContext.Employees select employees;
            DbContext.Employees.RemoveRange(all);
            DbContext.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            DbContext.Employees.Remove(employee);
            DbContext.SaveChanges();
        }

        public void DeleteAllRentals()
        {
            var all = from rentals in DbContext.Rentals select rentals;
            DbContext.Rentals.RemoveRange(all);
            DbContext.SaveChanges();
        }

        public void DeleteRentals(Rental rental)
        {
            DbContext.Rentals.Remove(rental);
            DbContext.SaveChanges();
        }
    }
}
