using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFleetManager.Migrations
{
    /// <inheritdoc />
    public partial class FixRentedVehicleNavigationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentingVehicleId",
                table: "Rentals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentingVehicleId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
