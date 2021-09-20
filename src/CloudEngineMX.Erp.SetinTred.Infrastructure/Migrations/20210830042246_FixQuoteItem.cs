using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class FixQuoteItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "CustomerQuoteDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scope",
                table: "CustomerQuoteDetails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerQuoteLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    QuoteId = table.Column<int>(nullable: false),
                    Movement = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CustomerQuoteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuoteLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQuoteLog_CustomerQuotes_CustomerQuoteId",
                        column: x => x.CustomerQuoteId,
                        principalTable: "CustomerQuotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerQuoteLog_UserCores_UserId",
                        column: x => x.UserId,
                        principalTable: "UserCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuoteLog_CustomerQuoteId",
                table: "CustomerQuoteLog",
                column: "CustomerQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuoteLog_UserId",
                table: "CustomerQuoteLog",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerQuoteLog");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "CustomerQuoteDetails");

            migrationBuilder.DropColumn(
                name: "Scope",
                table: "CustomerQuoteDetails");
        }
    }
}
