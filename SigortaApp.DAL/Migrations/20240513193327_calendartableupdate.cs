using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigortaApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class calendartableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Calendar",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Calendar",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Calendar",
                newName: "start");

            migrationBuilder.RenameColumn(
                name: "IsAllDay",
                table: "Calendar",
                newName: "allDay");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Calendar",
                newName: "end");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Calendar",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "color",
                table: "Calendar",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "start",
                table: "Calendar",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "end",
                table: "Calendar",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "allDay",
                table: "Calendar",
                newName: "IsAllDay");
        }
    }
}
