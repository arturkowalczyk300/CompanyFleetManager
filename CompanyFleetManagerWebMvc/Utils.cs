using CompanyFleetManager;
using CompanyFleetManager.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CompanyFleetManagerWebApp
{
    public class Utils
    {
        public static SeedingError? SeedData(FleetDatabaseContext dbContext) //returns tuple <isSuccessful, optional error>
        {
            var dbAccess = new FleetDatabaseAccess(dbContext);

            //check if database is empty
            if (dbAccess.GetEmployees().Count() > 0 || dbAccess.GetVehicles().Count() > 0 || dbAccess.GetRentals().Count() > 0)
                return new SeedingError("Database is not empty!");

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Employees ON");
                    dbAccess.AddEmployees(
                    new Employee(1, "Driver", "Wrocław Kwiatowa 1", "Janusz", "Mariusz", "Kowalski", 73213242123, new CompanyFleetManager.Models.PhoneNumber(48, 987654321),
                        null, new List<string> { "A", "B", "C", "T" }, new DateOnly(2030, 9, 1), new DateOnly(2027, 9, 9)),
                    new Employee(2, "Manager", "Trzebnica Milicka 34", "Andrzej", null, "Dobrzański", 67218242123, new CompanyFleetManager.Models.PhoneNumber(48, 527656421),
                        new CompanyFleetManager.Models.PhoneNumber(48, 537216421), new List<string> { "A2", "B" }, new DateOnly(2035, 2, 21), new DateOnly(2032, 3, 11)),
                    new Employee(3, "Security Guard", "Żórawina Dworcowa 23", "Aleksander", "Paweł", "Nowak", 83213452123, new CompanyFleetManager.Models.PhoneNumber(48, 327654321),
                        null, new List<string> { "A", "B", "C", "D" }, new DateOnly(2027, 6, 2), new DateOnly(2026, 2, 21))
                    );
                    dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Employees OFF");

                    dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Vehicles ON");
                    dbAccess.AddVehicles(
                    new Vehicle(1, "Audi", "A6", "DW 321AB", 2024, 24002, new DateOnly(2026, 9, 11), false),
                    new Vehicle(2, "BMW", "530D", "DW BX341", 2023, 44032, new DateOnly(2025, 1, 6), false),
                    new Vehicle(3, "Skoda", "Fabia", "WE 31DA3", 2020, 124202, new DateOnly(2025, 2, 1), false)
                    );
                    dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Vehicles OFF");

                    dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Rentals ON");
                    dbAccess.AddRentals(
                    new Rental(1, 1, 1, new DateOnly(2024, 8, 20), new DateTime(2024, 9, 24, 12, 01, 00), null), //not returned yet
                    new Rental(2, 2, 2, new DateOnly(2024, 8, 24), new DateTime(2024, 9, 28, 15, 21, 00), null),
                    new Rental(3, 3, 3, new DateOnly(2024, 9, 1), new DateTime(2024, 10, 15, 9, 5, 00), null)
                    );
                    dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Rentals OFF");

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new SeedingError(ex.ToString());
                }
            }
            return null; //success
        }

        public static string GetNamesOfNonValidEntries(ModelStateDictionary modelState)
        {
            var nonValid = modelState.Where(x => x.Value.ValidationState == ModelValidationState.Invalid);
            var nonValidNamesStr = String.Join(',', nonValid.Select(x => x.Key));
            return nonValidNamesStr;
        }
    }
}
