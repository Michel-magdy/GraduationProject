using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBusinessRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tours_BusinessId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_BusinessId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_BusinessId",
                table: "Hotels");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_BusinessId",
                table: "Tours",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_BusinessId",
                table: "Restaurants",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_BusinessId",
                table: "Hotels",
                column: "BusinessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tours_BusinessId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_BusinessId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_BusinessId",
                table: "Hotels");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_BusinessId",
                table: "Tours",
                column: "BusinessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_BusinessId",
                table: "Restaurants",
                column: "BusinessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_BusinessId",
                table: "Hotels",
                column: "BusinessId",
                unique: true);
        }
    }
}
