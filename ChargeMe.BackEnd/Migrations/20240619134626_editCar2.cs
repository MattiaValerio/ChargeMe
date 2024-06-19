using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChargeMe.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class editCar2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Discriminator", "LicensePlate", "Model", "Year" },
                values: new object[,]
                {
                    { 1, "Tesla", "Car", "AB123CD", "Model S", "" },
                    { 2, "Tesla", "Car", "EF456GH", "Model 3", "" },
                    { 3, "Tesla", "Car", "IJ789KL", "Model X", "" },
                    { 4, "Tesla", "Car", "MN012OP", "Model Y", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
