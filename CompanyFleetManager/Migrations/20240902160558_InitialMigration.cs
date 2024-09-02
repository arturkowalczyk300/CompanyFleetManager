using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFleetManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Occupation = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Forename = table.Column<string>(type: "TEXT", nullable: false),
                    Middlename = table.Column<string>(type: "TEXT", nullable: true),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    NationalIdentityNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    WorkPhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    PrivatePhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    DrivingLicenseCategories = table.Column<string>(type: "TEXT", nullable: false),
                    DrivingLicenseValidity = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    HiredUntil = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RentedVehicleId = table.Column<int>(type: "INTEGER", nullable: false),
                    RentingEmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    RentalDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    PlannedReturningDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FactualReturningDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.RentalId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    LicencePlateNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ProductionYear = table.Column<int>(type: "INTEGER", nullable: false),
                    Mileage = table.Column<int>(type: "INTEGER", nullable: false),
                    VehicleInspectionValidity = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    IsDamaged = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
