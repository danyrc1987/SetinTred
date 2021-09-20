using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class MaterialInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ManufacturerName = table.Column<string>(maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    MaterialName = table.Column<string>(maxLength: 150, nullable: false),
                    ManufacturerId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasurementMaterials_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialInventories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: false),
                    Serial = table.Column<string>(maxLength: 50, nullable: false),
                    Resolution = table.Column<string>(maxLength: 50, nullable: true),
                    DateOfCalibration = table.Column<DateTime>(type: "Date", nullable: true),
                    DueDateCalibration = table.Column<DateTime>(type: "Date", nullable: true),
                    CalibrationFrequency = table.Column<int>(nullable: true),
                    State = table.Column<string>(maxLength: 50, nullable: true),
                    Location = table.Column<string>(maxLength: 50, nullable: true),
                    InactiveSince = table.Column<DateTime>(type: "Date", nullable: true),
                    ActiveFrom = table.Column<DateTime>(type: "Date", nullable: true),
                    CertificationNumber = table.Column<string>(maxLength: 50, nullable: true),
                    HeadOfVerification = table.Column<string>(maxLength: 100, nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    MaterialId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialInventories_MeasurementMaterials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "MeasurementMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInventories_MaterialId",
                table: "MaterialInventories",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementMaterials_ManufacturerId",
                table: "MeasurementMaterials",
                column: "ManufacturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialInventories");

            migrationBuilder.DropTable(
                name: "MeasurementMaterials");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
