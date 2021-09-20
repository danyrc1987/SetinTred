using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class ReportDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportToPayDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    PurchaseOrderCode = table.Column<string>(nullable: true),
                    Applicant = table.Column<string>(nullable: true),
                    AreaName = table.Column<string>(nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    OperatingBaseName = table.Column<string>(nullable: true),
                    Application = table.Column<string>(nullable: true),
                    KeyDescription = table.Column<string>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    FiscalName = table.Column<string>(nullable: true),
                    IsCritical = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportToPayDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportToPayDetails");
        }
    }
}
