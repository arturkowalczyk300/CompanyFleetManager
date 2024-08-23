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
        private DatabaseContext dbContext = new DatabaseContext();

        public DatabaseAccess() { }

        public void AddVehicle(Vehicle vehicle)
        {
            dbContext.Vehicles.Add(vehicle);
            dbContext.SaveChanges();
        }

        public void AddEmployee(Employee employee)
        {
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
        }

        public void AddRental(Rental rental)
        {
            dbContext.Rentals.Add(rental);
            dbContext.SaveChanges();
        }

        public List<Vehicle> GetVehicles() => dbContext.Vehicles.ToList();

        public List<Employee> GetEmployees() => dbContext.Employees.ToList();

        public List<Rental> GetRentals() => dbContext.Rentals.ToList();

        public void DeleteVehicle(Vehicle vehicle)
        {
            dbContext.Vehicles.Remove(vehicle);
            dbContext.SaveChanges();
        }

        public void DeleteAllVehicles()
        {
            var all = from vehicles in dbContext.Vehicles select vehicles;
            dbContext.Vehicles.RemoveRange(all);
            dbContext.SaveChanges();
        }

        public void DeleteAllEmployees()
        {
            var all = from employees in dbContext.Employees select employees;
            dbContext.Employees.RemoveRange(all);
            dbContext.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
        }

        public void DeleteAllRentals()
        {
            var all = from rentals in dbContext.Rentals select rentals;
            dbContext.Rentals.RemoveRange(all);
            dbContext.SaveChanges();
        }

        public void DeleteRentals(Rental rental)
        {
            dbContext.Rentals.Remove(rental);
            dbContext.SaveChanges();
        }
    }
}
