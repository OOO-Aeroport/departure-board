using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartureBoard.Infrastructure.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ReduceAirplane : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentFuel",
                table: "Airplanes");

            migrationBuilder.DropColumn(
                name: "MaxFuel",
                table: "Airplanes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentFuel",
                table: "Airplanes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxFuel",
                table: "Airplanes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
