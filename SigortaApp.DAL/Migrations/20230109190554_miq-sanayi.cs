using Microsoft.EntityFrameworkCore.Migrations;

namespace SigortaApp.DAL.Migrations
{
    public partial class miqsanayi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Types",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Types_CategoryId",
                table: "Types",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Categories_CategoryId",
                table: "Types",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Categories_CategoryId",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_CategoryId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Types");
        }
    }
}
