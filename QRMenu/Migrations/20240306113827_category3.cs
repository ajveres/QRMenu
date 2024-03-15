using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRMenu.Migrations
{
    public partial class category3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RestaurantId",
                table: "Categories",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Restaurants_RestaurantId",
                table: "Categories",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Restaurants_RestaurantId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RestaurantId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Categories");
        }
    }
}
