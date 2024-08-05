using Microsoft.EntityFrameworkCore.Migrations;

namespace SigortaApp.DAL.Migrations
{
    public partial class miq_add_devicestable_isactive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Devices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Devices");
        }
    }
}
