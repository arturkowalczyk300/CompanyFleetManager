using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFleetManager.Models.Entities
{
    public class Rental
    {

        [Key]
        public int RentalId { get; set; }

        [ForeignKey("VehicleId")]
        public int RentedVehicleId { get; set; }

        [ForeignKey("EmployeeId")]
        public int RentingEmployeeId { get; set; }

        public DateOnly RentalDate { get; set; }

        public DateTime PlannedReturningDate { get; set; }
        public DateTime? FactualReturningDate { get; set; }

        public Rental()
        {

        }

        public Rental(int rentalId, int rentedVehicleId, int rentingEmployeeId, DateOnly rentalDate, DateTime plannedReturningDate, DateTime? factualReturningDate)
        {
            RentalId = rentalId;
            RentedVehicleId = rentedVehicleId;
            RentingEmployeeId = rentingEmployeeId;
            RentalDate = rentalDate;
            PlannedReturningDate = plannedReturningDate;
            FactualReturningDate = factualReturningDate;
        }
    }
}
