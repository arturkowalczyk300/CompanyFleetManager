using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFleetManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialMsSqlServerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Middlename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalIdentityNumber = table.Column<long>(type: "bigint", nullable: false),
                    WorkPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrivatePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingLicenseCategories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrivingLicenseValidity = table.Column<DateOnly>(type: "date", nullable: false),
                    HiredUntil = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicencePlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionYear = table.Column<int>(type: "int", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    VehicleInspectionValidity = table.Column<DateOnly>(type: "date", nullable: false),
                    IsDamaged = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentedVehicleId = table.Column<int>(type: "int", nullable: false),
                    RentingEmployeeId = table.Column<int>(type: "int", nullable: false),
                    RentalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PlannedReturningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FactualReturningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RentingVehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_Rentals_Employees_RentingEmployeeId",
                        column: x => x.RentingEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Vehicles_RentedVehicleId",
                        column: x => x.RentedVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentedVehicleId",
                table: "Rentals",
                column: "RentedVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentingEmployeeId",
                table: "Rentals",
                column: "RentingEmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
