using Microsoft.EntityFrameworkCore.Migrations;

namespace SigortaApp.DAL.Migrations
{
    public partial class miq_addcolumns_taskandpaymenttask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyWillPayId",
                table: "Task",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "PaymentTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTask_CompanyId",
                table: "PaymentTask",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTask_Company_CompanyId",
                table: "PaymentTask",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTask_Company_CompanyId",
                table: "PaymentTask");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTask_CompanyId",
                table: "PaymentTask");

            migrationBuilder.DropColumn(
                name: "CompanyWillPayId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PaymentTask");
        }
    }
}
