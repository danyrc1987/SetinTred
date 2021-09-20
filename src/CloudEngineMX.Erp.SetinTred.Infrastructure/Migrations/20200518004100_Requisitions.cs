using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class Requisitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_SpecificationTypes_SpecificationTypeId",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_SpecificationTypeId",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "SpecificationTypeId",
                table: "Specifications");

            migrationBuilder.AlterColumn<string>(
                name: "SpecificationTypeName",
                table: "SpecificationTypes",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "SpecificationTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SpecificationTypes",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "SpecificationName",
                table: "Specifications",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Specifications",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Specifications",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "OperatingBaseName",
                table: "OperatingBases",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "OperatingBases",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "OperatingBases",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ConfidenceLevels",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "ConfidenceLevels",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ConfidenceLevelName",
                table: "ConfidenceLevels",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Requisitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RequisitionCode = table.Column<string>(nullable: true),
                    AreaId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    OperatingBaseId = table.Column<int>(nullable: false),
                    Classification = table.Column<string>(nullable: true),
                    Application = table.Column<string>(nullable: true),
                    UserCoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requisitions_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requisitions_OperatingBases_OperatingBaseId",
                        column: x => x.OperatingBaseId,
                        principalTable: "OperatingBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requisitions_UserCores_UserCoreId",
                        column: x => x.UserCoreId,
                        principalTable: "UserCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequisitionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RequisitionId = table.Column<int>(nullable: false),
                    Consecutive = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    MeasurementUnit = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Specification = table.Column<string>(nullable: true),
                    Review = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequisitionDetails_Requisitions_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_IdSpecificationType",
                table: "Specifications",
                column: "IdSpecificationType");

            migrationBuilder.CreateIndex(
                name: "IX_RequisitionDetails_RequisitionId",
                table: "RequisitionDetails",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_AreaId",
                table: "Requisitions",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_OperatingBaseId",
                table: "Requisitions",
                column: "OperatingBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_UserCoreId",
                table: "Requisitions",
                column: "UserCoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_SpecificationTypes_IdSpecificationType",
                table: "Specifications",
                column: "IdSpecificationType",
                principalTable: "SpecificationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_SpecificationTypes_IdSpecificationType",
                table: "Specifications");

            migrationBuilder.DropTable(
                name: "RequisitionDetails");

            migrationBuilder.DropTable(
                name: "Requisitions");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_IdSpecificationType",
                table: "Specifications");

            migrationBuilder.AlterColumn<string>(
                name: "SpecificationTypeName",
                table: "SpecificationTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "SpecificationTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SpecificationTypes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "SpecificationName",
                table: "Specifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Specifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Specifications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AddColumn<int>(
                name: "SpecificationTypeId",
                table: "Specifications",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OperatingBaseName",
                table: "OperatingBases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "OperatingBases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "OperatingBases",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ConfidenceLevels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "ConfidenceLevels",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "ConfidenceLevelName",
                table: "ConfidenceLevels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_SpecificationTypeId",
                table: "Specifications",
                column: "SpecificationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_SpecificationTypes_SpecificationTypeId",
                table: "Specifications",
                column: "SpecificationTypeId",
                principalTable: "SpecificationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
