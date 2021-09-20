using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class AddCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelComments",
                table: "PurchaseOrders",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PurchaseOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresVat",
                table: "PurchaseOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TotalInLetters",
                table: "PurchaseOrders",
                maxLength: 700,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CurrencyName = table.Column<string>(maxLength: 50, nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 50, nullable: false),
                    CurrencySymbol = table.Column<string>(maxLength: 50, nullable: false),
                    ExchangeRate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CurrencyId",
                table: "PurchaseOrders",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Currencies_CurrencyId",
                table: "PurchaseOrders",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Currencies_CurrencyId",
                table: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_CurrencyId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "CancelComments",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "RequiresVat",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "TotalInLetters",
                table: "PurchaseOrders");
        }
    }
}
