using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class AddPreQuotationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreQuotations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RouteFile = table.Column<string>(maxLength: 500, nullable: false),
                    FileName = table.Column<string>(maxLength: 500, nullable: false),
                    RequisitionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreQuotations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreQuotations_Requisitions_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreQuotations_RequisitionId",
                table: "PreQuotations",
                column: "RequisitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreQuotations");
        }
    }
}
