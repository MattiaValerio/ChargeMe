using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChargeMe.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ChargingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChargingPrice",
                table: "Cars",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "LeastTime",
                table: "Cars",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Voltage",
                table: "Cars",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargingPrice",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "LeastTime",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Voltage",
                table: "Cars");
        }
    }
}
