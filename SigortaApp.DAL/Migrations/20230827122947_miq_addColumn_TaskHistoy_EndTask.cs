using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SigortaApp.DAL.Migrations
{
    public partial class miq_addColumn_TaskHistoy_EndTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "End_BusDate",
                table: "TaskHistory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "End_BusPhone",
                table: "TaskHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "End_BusPrice",
                table: "TaskHistory",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End_BusDate",
                table: "TaskHistory");

            migrationBuilder.DropColumn(
                name: "End_BusPhone",
                table: "TaskHistory");

            migrationBuilder.DropColumn(
                name: "End_BusPrice",
                table: "TaskHistory");
        }
    }
}
