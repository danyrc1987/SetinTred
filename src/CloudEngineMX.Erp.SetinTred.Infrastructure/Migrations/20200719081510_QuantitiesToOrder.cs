using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class QuantitiesToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityDispatched",
                table: "RequisitionDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantityToBuy",
                table: "RequisitionDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityDispatched",
                table: "RequisitionDetails");

            migrationBuilder.DropColumn(
                name: "QuantityToBuy",
                table: "RequisitionDetails");
        }
    }
}
