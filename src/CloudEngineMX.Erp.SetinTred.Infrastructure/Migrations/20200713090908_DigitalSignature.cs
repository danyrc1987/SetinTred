using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class DigitalSignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DigitalSignature",
                table: "PurchaseOrders",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DigitalSignature",
                table: "PurchaseOrders");
        }
    }
}
