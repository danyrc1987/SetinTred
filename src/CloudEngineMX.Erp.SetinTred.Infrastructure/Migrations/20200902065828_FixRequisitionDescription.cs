using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class FixRequisitionDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyDescription",
                table: "Requisitions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyDescription",
                table: "PurchaseOrders",
                maxLength: 1500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyDescription",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "KeyDescription",
                table: "PurchaseOrders");
        }
    }
}
