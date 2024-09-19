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
    public class FleetDatabaseContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public static string ServerAddress { get; set; } = $"DESKTOP-1B6DSC3\\SQLEXPRESS";
        public static string DatabaseName { get; set; } = "Fleet";
        public static string ConnectionString { get; set; } = $"Server={ServerAddress};Database={DatabaseName};Trusted_Connection=True;Encrypt=False";

        public FleetDatabaseContext()
        {
            
        }

        public FleetDatabaseContext(DbContextOptions<FleetDatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
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

            //configure relations
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.RentingEmployee)
                .WithMany(e => e.Rentals)
                .HasForeignKey(r => r.RentingEmployeeId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.RentedVehicle)
                .WithMany(v => v.Rentals)
                .HasForeignKey(r => r.RentedVehicleId);
        }
    }
}
