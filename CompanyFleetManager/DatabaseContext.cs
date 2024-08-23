using CompanyFleetManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFleetManager
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public string DbPath { get; }

        public DatabaseContext()
        {
            DbPath = "fleet.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source={DbPath}");
        }
    }
}
