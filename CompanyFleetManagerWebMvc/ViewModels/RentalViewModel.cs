using CompanyFleetManager.Models.Entities;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;

namespace CompanyFleetManagerWebApp.ViewModels
{
    public class RentalViewModel
    {
        public RentalViewModel(Rental rental, ShortenedEmployeeData? employee, ShortenedVehicleData? vehicle)
        {
            Rental = rental;
            Employee = employee;
            Vehicle = vehicle;
        }

        public Rental Rental { get; set; }
        public ShortenedEmployeeData? Employee { get; set; }
        public ShortenedVehicleData? Vehicle { get; set; }
    }

    public class ShortenedVehicleData
    {
        public ShortenedVehicleData(Vehicle vehicle)
        {
            Brand = vehicle.Brand;
            Model = vehicle.Model;
            LicencePlateNumber = vehicle.LicencePlateNumber;
        }

        public ShortenedVehicleData(string brand, string model, string licencePlateNumber)
        {
            Brand = brand;
            Model = model;
            LicencePlateNumber = licencePlateNumber;
        }

        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicencePlateNumber { get; set; }

        public override string ToString() => $"{Brand} {Model}, {LicencePlateNumber}";
    }

    public class ShortenedEmployeeData
    {
        public ShortenedEmployeeData(Employee employee)
        {
            Forename = employee.Forename;
            Middlename = employee.Middlename;
            Surname = employee.Surname;
            Occupation = employee.Occupation;
        }

        public ShortenedEmployeeData(string forename, string? middlename, string surname, string occupation)
        {
            Forename = forename;
            Middlename = middlename;
            Surname = surname;
            Occupation = occupation;
        }

        public string Forename { get; set; }
        public string? Middlename { get; set; }
        public string Surname { get; set; }
        public string Occupation { get; set; }

        public override string ToString() => $"{Forename} {Middlename} {Surname}, {Occupation}";
    }
}
