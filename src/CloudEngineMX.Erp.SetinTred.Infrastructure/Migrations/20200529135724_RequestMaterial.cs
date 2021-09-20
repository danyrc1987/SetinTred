using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class RequestMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RequestCode = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    IsDispatched = table.Column<bool>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    DispatchedDate = table.Column<DateTime>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: true),
                    UserCoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialRequests_UserCores_UserCoreId",
                        column: x => x.UserCoreId,
                        principalTable: "UserCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialRequestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    MeasurementMaterialId = table.Column<int>(nullable: false),
                    MaterialRequestId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    IsDispatched = table.Column<bool>(nullable: false),
                    IsEntry = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialRequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialRequestDetails_MaterialRequests_MaterialRequestId",
                        column: x => x.MaterialRequestId,
                        principalTable: "MaterialRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialRequestDetails_MeasurementMaterials_MeasurementMaterialId",
                        column: x => x.MeasurementMaterialId,
                        principalTable: "MeasurementMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequestDetails_MaterialRequestId",
                table: "MaterialRequestDetails",
                column: "MaterialRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequestDetails_MeasurementMaterialId",
                table: "MaterialRequestDetails",
                column: "MeasurementMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_UserCoreId",
                table: "MaterialRequests",
                column: "UserCoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialRequestDetails");

            migrationBuilder.DropTable(
                name: "MaterialRequests");
        }
    }
}
