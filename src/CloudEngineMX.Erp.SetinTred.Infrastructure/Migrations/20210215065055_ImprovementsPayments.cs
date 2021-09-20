using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class ImprovementsPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Payments",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SupplierId",
                table: "Payments",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Suppliers_SupplierId",
                table: "Payments",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Suppliers_SupplierId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SupplierId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Payments");
        }
    }
}
