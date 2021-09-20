using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class FixUserApprover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "UserCores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserCores_ReportId",
                table: "UserCores",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCores_UserCores_ReportId",
                table: "UserCores",
                column: "ReportId",
                principalTable: "UserCores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCores_UserCores_ReportId",
                table: "UserCores");

            migrationBuilder.DropIndex(
                name: "IX_UserCores_ReportId",
                table: "UserCores");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "UserCores");
        }
    }
}
