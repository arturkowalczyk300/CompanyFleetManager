using CompanyFleetManager.Models;
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

        public static string DatabaseFilename { get; set; } = "fleet.db";
        public static string ConnectionString { get; set; } = $"Data Source={DatabaseFilename}";


        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Employee>()
                .Property(e => e.PrivatePhoneNumber)
                .HasConversion(
                v => v.ToString(),
                v => PhoneNumber.ParseString(v));

            modelBuilder
                .Entity<Employee>()
                .Property(e => e.WorkPhoneNumber)
                .HasConversion(
                v => v.ToString(),
                v => PhoneNumber.ParseString(v));
        }
    }
}
