using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFleetManager.Models.Entities
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicencePlateNumber { get; set; }
        public int ProductionYear { get; set; }
        public int Mileage { get; set; }
        public DateOnly VehicleInspectionValidity { get; set; }
        public bool IsDamaged { get; set; }

        //navigation property
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

        public Vehicle()
        {
            Model = "";
            Brand = "";
            LicencePlateNumber = "";
        }

        public Vehicle(
            int vehicleId,
            string brand,
            string model,
            string licencePlateNumber,
            int productionYear,
            int mileage,
            DateOnly vehicleInspectionValidity,
            bool isDamaged)
        {
            VehicleId = vehicleId;
            Brand = brand;
            Model = model;
            LicencePlateNumber = licencePlateNumber;
            ProductionYear = productionYear;
            Mileage = mileage;
            VehicleInspectionValidity = vehicleInspectionValidity;
            IsDamaged = isDamaged;
        }

    }
}
