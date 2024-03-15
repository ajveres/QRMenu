using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRMenu.Migrations
{
    public partial class category2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateId = table.Column<byte>(type: "tinyint", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    PostalCode = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Restaurants_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CompanyId",
                table: "Restaurants",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_StateId",
                table: "Restaurants",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
