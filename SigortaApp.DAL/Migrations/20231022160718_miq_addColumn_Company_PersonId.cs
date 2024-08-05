using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigortaApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class miq_addColumn_Company_PersonId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Company",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Company");
        }
    }
}
