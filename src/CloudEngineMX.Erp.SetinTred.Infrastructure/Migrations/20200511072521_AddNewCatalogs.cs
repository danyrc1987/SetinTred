using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class AddNewCatalogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceOfReception",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "TrustLevel",
                table: "Suppliers");

            migrationBuilder.AddColumn<int>(
                name: "IdConfidenceLevel",
                table: "Suppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdOperatingBase",
                table: "Suppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ConfidenceLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ConfidenceLevelName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfidenceLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatingBases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    OperatingBaseName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingBases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    SpecificationTypeName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    SpecificationName = table.Column<string>(nullable: true),
                    IdSpecificationType = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SpecificationTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specifications_SpecificationTypes_SpecificationTypeId",
                        column: x => x.SpecificationTypeId,
                        principalTable: "SpecificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_IdConfidenceLevel",
                table: "Suppliers",
                column: "IdConfidenceLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_IdOperatingBase",
                table: "Suppliers",
                column: "IdOperatingBase");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_SpecificationTypeId",
                table: "Specifications",
                column: "SpecificationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_ConfidenceLevels_IdConfidenceLevel",
                table: "Suppliers",
                column: "IdConfidenceLevel",
                principalTable: "ConfidenceLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_OperatingBases_IdOperatingBase",
                table: "Suppliers",
                column: "IdOperatingBase",
                principalTable: "OperatingBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_ConfidenceLevels_IdConfidenceLevel",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_OperatingBases_IdOperatingBase",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "ConfidenceLevels");

            migrationBuilder.DropTable(
                name: "OperatingBases");

            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "SpecificationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_IdConfidenceLevel",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_IdOperatingBase",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IdConfidenceLevel",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IdOperatingBase",
                table: "Suppliers");

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfReception",
                table: "Suppliers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrustLevel",
                table: "Suppliers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
