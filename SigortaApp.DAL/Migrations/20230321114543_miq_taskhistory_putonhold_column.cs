using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SigortaApp.DAL.Migrations
{
    public partial class miq_taskhistory_putonhold_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PutOnHoldDate",
                table: "TaskHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PutOnHoldDate",
                table: "TaskHistory");
        }
    }
}
