using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCarRentalRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarRentals_BusinessId",
                table: "CarRentals");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_BusinessId",
                table: "CarRentals",
                column: "BusinessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarRentals_BusinessId",
                table: "CarRentals");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_BusinessId",
                table: "CarRentals",
                column: "BusinessId",
                unique: true);
        }
    }
}
