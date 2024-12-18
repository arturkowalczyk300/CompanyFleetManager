﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFleetManager.Models.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string Forename { get; set; }
        public string? Middlename { get; set; }
        public string Surname { get; set; }
        public long NationalIdentityNumber { get; set; }
        public PhoneNumber WorkPhoneNumber { get; set; }
        public PhoneNumber? PrivatePhoneNumber { get; set; }
        public List<string> DrivingLicenseCategories { get; set; }
        public DateOnly DrivingLicenseValidity { get; set; }
        public DateOnly HiredUntil { get; set; }

        //navigation property
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

        //Converter property for binding in view
        [NotMapped]
        public string DrivingLicenseCategoriesText => String.Join(",", DrivingLicenseCategories);

        public Employee()
        {
            Occupation = "";
            Address = "";
            Forename = "";
            Surname = "";
            WorkPhoneNumber = new PhoneNumber(0, 0);
            DrivingLicenseCategories = new List<string>() { };
        }
            
        public Employee(int employeeId,
                        string occupation,
                        string address,
                        string forename,
                        string? middlename,
                        string surname,
                        long nationalIdentityNumber,
                        PhoneNumber workPhoneNumber,
                        PhoneNumber? privatePhoneNumber,
                        List<string> drivingLicenseCategories,
                        DateOnly drivingLicenseValidity,
                        DateOnly hiredUntil)
        {
            EmployeeId = employeeId;
            Occupation = occupation;
            Address = address;
            Forename = forename;
            Middlename = middlename;
            Surname = surname;
            NationalIdentityNumber = nationalIdentityNumber;
            WorkPhoneNumber = workPhoneNumber;
            PrivatePhoneNumber = privatePhoneNumber;
            DrivingLicenseCategories = drivingLicenseCategories;
            DrivingLicenseValidity = drivingLicenseValidity;
            HiredUntil = hiredUntil;
        }
    }
}
