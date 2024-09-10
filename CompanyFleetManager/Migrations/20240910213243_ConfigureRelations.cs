using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFleetManager.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentingVehicleId",
                table: "Rentals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentedVehicleId",
                table: "Rentals",
                column: "RentedVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentingEmployeeId",
                table: "Rentals",
                column: "RentingEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Employees_RentingEmployeeId",
                table: "Rentals",
                column: "RentingEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Vehicles_RentedVehicleId",
                table: "Rentals",
                column: "RentedVehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Employees_RentingEmployeeId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Vehicles_RentedVehicleId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_RentedVehicleId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_RentingEmployeeId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentingVehicleId",
                table: "Rentals");
        }
    }
}
