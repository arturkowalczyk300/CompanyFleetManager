using CompanyFleetManager;
using CompanyFleetManager.Models;
using CompanyFleetManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFleetManagerTests
{
    public class DatabaseTests
    {
        private DbContextOptions<FleetDatabaseContext> contextOptions;

        public DatabaseTests()
        {
            contextOptions = new DbContextOptionsBuilder<FleetDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "FleetInMemoryDatabase")
                .Options;
        }

        private FleetDatabaseContext CreateContext()
        {
            var context = new FleetDatabaseContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void AddVehicle_SuccessfullyAddsVehicleToDatabase()
        {
            using (var context = CreateContext())
            {
                var vehicle = new Vehicle(
                    vehicleId: 0,
                    brand: "Audi",
                    model: "R8",
                    licencePlateNumber: "DWLAB01",
                    productionYear: 2019,
                    mileage: 210000,
                    vehicleInspectionValidity: new DateOnly(2024, 10, 2),
                    isDamaged: false
                    );

                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddVehicle(vehicle);

                var result = context.Vehicles.ToList();

                Assert.Single(result);
                Assert.Equal(vehicle.Model, result.First().Model);
                Assert.Equal(vehicle.LicencePlateNumber, result.First().LicencePlateNumber);
            }
        }

        [Fact]
        public void AddVehicles_SuccessfullyAddsVehiclesToDatabase()
        {
            using (var context = CreateContext())
            {
                var vehicle1 = new Vehicle(1, "Audi", "A6", "DW 321AB", 2024, 24002, new DateOnly(2026, 9, 11), false);
                var vehicle2 = new Vehicle(2, "BMW", "530D", "DW BX341", 2023, 44032, new DateOnly(2025, 1, 6), false);
                var vehicle3 = new Vehicle(3, "Skoda", "Fabia", "WE 31DA3", 2020, 124202, new DateOnly(2025, 2, 1), false);

                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddVehicles(vehicle1, vehicle2, vehicle3);

                var result = context.Vehicles.ToList();

                Assert.Equal(3, result.Count);
                Assert.Equal(vehicle1.Model, result.First().Model);
                Assert.Equal(vehicle1.LicencePlateNumber, result.First().LicencePlateNumber);
                Assert.Equal(vehicle3.Model, result.Last().Model);
                Assert.Equal(vehicle3.LicencePlateNumber, result.Last().LicencePlateNumber);

            }
        }

        [Fact]
        public void AddEmployee_SuccessfullyAddsEmployeeToDatabase()
        {
            using (var context = CreateContext())
            {
                var employee = new Employee(
                    employeeId: 0,
                    occupation: "Driver",
                    address: "Wrocław Kwiatowa 4",
                    forename: "Zbigniew",
                    middlename: null,
                    surname: "Kowalski",
                    nationalIdentityNumber: 12345678912,
                    workPhoneNumber: new PhoneNumber(48, 123456789),
                    privatePhoneNumber: new PhoneNumber(48, 987654321),
                    drivingLicenseCategories: new List<string>() { "A", "B", "C" },
                    drivingLicenseValidity: new DateOnly(2030, 11, 20),
                    hiredUntil: new DateOnly(2025, 1, 1)
                    );

                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddEmployee(employee);

                var result = context.Employees.ToList();
                Assert.Single(result);
                Assert.Equal(employee.Surname, result.First().Surname);
                Assert.Equal(employee.NationalIdentityNumber, result.First().NationalIdentityNumber);
            }
        }

        [Fact]
        public void AddEmployees_SuccessfullyAddsEmployeesToDatabase()
        {
            using (var context = CreateContext())
            {
                var employee1 = new Employee(1, "Driver", "Wrocław Kwiatowa 1", "Janusz", "Mariusz", "Kowalski", 73213242123,
                    new PhoneNumber(48, 987654321), null, new List<string> { "A", "B", "C", "T" }, new DateOnly(2030, 9, 1), new DateOnly(2027, 9, 9));
                var employee2 = new Employee(2, "Manager", "Trzebnica Milicka 34", "Andrzej", null, "Dobrzański", 67218242123,
                    new PhoneNumber(48, 527656421), new PhoneNumber(48, 537216421), new List<string> { "A2", "B" }, new DateOnly(2035, 2, 21), new DateOnly(2032, 3, 11));
                var employee3 = new Employee(3, "Security Guard", "Żórawina Dworcowa 23", "Aleksander", "Paweł", "Nowak", 83213452123,
                    new PhoneNumber(48, 327654321), null, new List<string> { "A", "B", "C", "D" }, new DateOnly(2027, 6, 2), new DateOnly(2026, 2, 21));

                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddEmployees(employee1, employee2, employee3);

                var result = context.Employees.ToList();

                Assert.Equal(3, result.Count);
                Assert.Equal(employee1.Surname, result.First().Surname);
                Assert.Equal(employee1.NationalIdentityNumber, result.First().NationalIdentityNumber);
                Assert.Equal(employee3.Surname, result.Last().Surname);
                Assert.Equal(employee3.NationalIdentityNumber, result.Last().NationalIdentityNumber);
            }
        }

        [Fact]
        public void AddRental_SuccessfullyAddsRentalToDatabase()
        {
            using (var context = CreateContext())
            {
                var rental = new Rental(
                    rentalId: 0,
                    rentedVehicleId: 1,
                    rentingEmployeeId: 2,
                    rentalDate: new DateOnly(2024, 9, 2),
                    plannedReturningDate: new DateTime(2024, 10, 01),
                    factualReturningDate: null);

                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddRental(rental);

                var result = databaseAccess.GetRentals().ToList();

                Assert.Single(result);
                Assert.Equal(rental.RentedVehicleId, result.First().RentedVehicleId);
                Assert.Equal(rental.RentingEmployeeId, result.First().RentingEmployeeId);
            }
        }


        [Fact]
        public void AddRentals_SuccessfullyAddsRentalsToDatabase()
        {
            using (var context = CreateContext())
            {
                var rental1 = new Rental(1, 1, 1, new DateOnly(2024, 8, 20), new DateTime(2024, 9, 24, 12, 01, 00), null);
                var rental2 = new Rental(2, 2, 2, new DateOnly(2024, 8, 24), new DateTime(2024, 9, 28, 15, 21, 00), null);
                var rental3 = new Rental(3, 3, 3, new DateOnly(2024, 9, 1), new DateTime(2024, 10, 15, 9, 5, 00), null);

                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddRentals(rental1, rental2, rental3);

                var result = databaseAccess.GetRentals().ToList();

                Assert.Equal(3, result.Count);
                Assert.Equal(rental1.RentedVehicleId, result.First().RentedVehicleId);
                Assert.Equal(rental1.RentingEmployeeId, result.First().RentingEmployeeId);
                Assert.Equal(rental3.RentedVehicleId, result.Last().RentedVehicleId);
                Assert.Equal(rental3.RentingEmployeeId, result.Last().RentingEmployeeId);
            }
        }

        [Fact]
        public void GetVehicles_ReturnAllVehicles()
        {
            using (var context = CreateContext())
            {
                var vehicle1 = new Vehicle(
                        vehicleId: 0,
                        brand: "Audi",
                        model: "R8",
                        licencePlateNumber: "DWLAB01",
                        productionYear: 2019,
                        mileage: 210000,
                        vehicleInspectionValidity: new DateOnly(2024, 10, 2),
                        isDamaged: false
                        );

                var vehicle2 = new Vehicle(
                        vehicleId: 0,
                        brand: "BMW",
                        model: "M5",
                        licencePlateNumber: "DTR09212",
                        productionYear: 2022,
                        mileage: 85000,
                        vehicleInspectionValidity: new DateOnly(2025, 7, 2),
                        isDamaged: false
                        );


                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddVehicle(vehicle1);
                databaseAccess.AddVehicle(vehicle2);

                var results = context.Vehicles.ToList();

                Assert.Equal(2, context.Vehicles.Count());
                Assert.Equal(vehicle1.LicencePlateNumber, results[0].LicencePlateNumber);
                Assert.Equal(vehicle2.LicencePlateNumber, results[1].LicencePlateNumber);
            }
        }

        [Fact]
        public void GetEmployees_ReturnsAllEmployees()
        {
            using (var context = CreateContext())
            {
                var employee1 = new Employee(
                    employeeId: 0,
                    occupation: "Driver",
                    address: "Wrocław Kwiatowa 4",
                    forename: "Zbigniew",
                    middlename: null,
                    surname: "Kowalski",
                    nationalIdentityNumber: 12345678912,
                    workPhoneNumber: new PhoneNumber(48, 123456789),
                    privatePhoneNumber: new PhoneNumber(48, 987654321),
                    drivingLicenseCategories: new List<string>() { "A", "B", "C" },
                    drivingLicenseValidity: new DateOnly(2030, 11, 20),
                    hiredUntil: new DateOnly(2025, 1, 1)
                    );

                var employee2 = new Employee(
                    employeeId: 0,
                    occupation: "Technician",
                    address: "Wrocław Opolska 2",
                    forename: "Marek",
                    middlename: null,
                    surname: "Nowak",
                    nationalIdentityNumber: 12344578912,
                    workPhoneNumber: new PhoneNumber(48, 127656789),
                    privatePhoneNumber: new PhoneNumber(48, 987632321),
                    drivingLicenseCategories: new List<string>() { "B", "C" },
                    drivingLicenseValidity: new DateOnly(2032, 11, 20),
                    hiredUntil: new DateOnly(2026, 3, 2)
                    );

                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddEmployee(employee1);
                databaseAccess.AddEmployee(employee2);

                var result = context.Employees.ToList();
                Assert.Equal(2, context.Employees.Count());
                Assert.Equal(employee1.NationalIdentityNumber, result[0].NationalIdentityNumber);
                Assert.Equal(employee2.NationalIdentityNumber, result[1].NationalIdentityNumber);
            }
        }
        [Fact]
        public void GetRentals_ReturnsAllRentals()
        {
            using (var context = CreateContext())
            {
                var rental1 = new Rental(
                    rentalId: 1,
                    rentedVehicleId: 1,
                    rentingEmployeeId: 2,
                    rentalDate: new DateOnly(2024, 9, 2),
                    plannedReturningDate: new DateTime(2024, 10, 01),
                    factualReturningDate: null);

                var rental2 = new Rental(
                    rentalId: 2,
                    rentedVehicleId: 5,
                    rentingEmployeeId: 3,
                    rentalDate: new DateOnly(2024, 8, 22),
                    plannedReturningDate: new DateTime(2024, 9, 2),
                    factualReturningDate: null);

                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddRental(rental1);
                databaseAccess.AddRental(rental2);

                var result = databaseAccess.GetRentals().ToList();

                Assert.Equal(2, context.Rentals.Count());
                Assert.Equal(rental1.RentalDate, result[0].RentalDate);
                Assert.Equal(rental2.RentalDate, result[1].RentalDate);
            }
        }

        [Fact]
        public void DeleteAllVehicles_SuccessfullyRemovesAllVehicles()
        {
            using (var context = CreateContext())
            {
                var vehicle1 = new Vehicle(
                        vehicleId: 0,
                        brand: "Audi",
                        model: "R8",
                        licencePlateNumber: "DWLAB01",
                        productionYear: 2019,
                        mileage: 210000,
                        vehicleInspectionValidity: new DateOnly(2024, 10, 2),
                        isDamaged: false
                        );

                var vehicle2 = new Vehicle(
                        vehicleId: 0,
                        brand: "BMW",
                        model: "M5",
                        licencePlateNumber: "DTR09212",
                        productionYear: 2022,
                        mileage: 85000,
                        vehicleInspectionValidity: new DateOnly(2025, 7, 2),
                        isDamaged: false
                        );


                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddVehicle(vehicle1);
                databaseAccess.AddVehicle(vehicle2);

                databaseAccess.DeleteAllVehicles();

                var results = context.Vehicles.ToList();
                Assert.Empty(results);
            }
        }

        [Fact]
        public void DeleteAllEmployees_SuccessfullyRemovesAllEmployees()
        {
            using (var context = CreateContext())
            {
                var employee1 = new Employee(
                    employeeId: 0,
                    occupation: "Driver",
                    address: "Wrocław Kwiatowa 4",
                    forename: "Zbigniew",
                    middlename: null,
                    surname: "Kowalski",
                    nationalIdentityNumber: 12345678912,
                    workPhoneNumber: new PhoneNumber(48, 123456789),
                    privatePhoneNumber: new PhoneNumber(48, 987654321),
                    drivingLicenseCategories: new List<string>() { "A", "B", "C" },
                    drivingLicenseValidity: new DateOnly(2030, 11, 20),
                    hiredUntil: new DateOnly(2025, 1, 1)
                    );

                var employee2 = new Employee(
                    employeeId: 0,
                    occupation: "Technician",
                    address: "Wrocław Opolska 2",
                    forename: "Marek",
                    middlename: null,
                    surname: "Nowak",
                    nationalIdentityNumber: 12344578912,
                    workPhoneNumber: new PhoneNumber(48, 127656789),
                    privatePhoneNumber: new PhoneNumber(48, 987632321),
                    drivingLicenseCategories: new List<string>() { "B", "C" },
                    drivingLicenseValidity: new DateOnly(2032, 11, 20),
                    hiredUntil: new DateOnly(2026, 3, 2)
                    );

                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddEmployee(employee1);
                databaseAccess.AddEmployee(employee2);

                databaseAccess.DeleteAllEmployees();

                var results = context.Employees.ToList();
                Assert.Empty(results);
            }
        }
        [Fact]
        public void DeleteAllRentals_SuccessfullyRemovesAllRentals()
        {
            using (var context = CreateContext())
            {
                var rental1 = new Rental(
                    rentalId: 1,
                    rentedVehicleId: 1,
                    rentingEmployeeId: 2,
                    rentalDate: new DateOnly(2024, 9, 2),
                    plannedReturningDate: new DateTime(2024, 10, 01),
                    factualReturningDate: null);

                var rental2 = new Rental(
                    rentalId: 2,
                    rentedVehicleId: 5,
                    rentingEmployeeId: 3,
                    rentalDate: new DateOnly(2024, 8, 22),
                    plannedReturningDate: new DateTime(2024, 9, 2),
                    factualReturningDate: null);

                var databaseAccess = new FleetDatabaseAccess(context);

                databaseAccess.AddRental(rental1);
                databaseAccess.AddRental(rental2);

                databaseAccess.DeleteAllRentals();

                var results = context.Rentals.ToList();
                Assert.Empty(results);
            }
        }
    }
}