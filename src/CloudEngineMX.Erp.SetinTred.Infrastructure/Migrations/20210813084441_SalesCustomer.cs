using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Migrations
{
    public partial class SalesCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "LiberationDate",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    FiscalName = table.Column<string>(maxLength: 150, nullable: false),
                    SocialName = table.Column<string>(maxLength: 150, nullable: false),
                    Rfc = table.Column<string>(maxLength: 13, nullable: false),
                    PersonType = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: false),
                    AdministrativeContactName = table.Column<string>(maxLength: 50, nullable: false),
                    AdministrativeContactEmail = table.Column<string>(maxLength: 50, nullable: false),
                    AdministrativeContactPhone = table.Column<string>(maxLength: 50, nullable: false),
                    FinancialContactName = table.Column<string>(maxLength: 50, nullable: false),
                    FinancialContactPhone = table.Column<string>(maxLength: 50, nullable: false),
                    FinancialContactEmail = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "date", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "date", nullable: false),
                    DocumentType = table.Column<string>(maxLength: 500, nullable: false),
                    DocumentName = table.Column<string>(maxLength: 500, nullable: false),
                    DocumentRoute = table.Column<string>(maxLength: 1500, nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDocuments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerQuotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    QuoteCode = table.Column<string>(maxLength: 25, nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    LegalDocumentation = table.Column<string>(maxLength: 500, nullable: false),
                    TechnicalData = table.Column<string>(maxLength: 500, nullable: false),
                    NormativeData = table.Column<string>(maxLength: 500, nullable: false),
                    ManufacturingStandard = table.Column<string>(maxLength: 500, nullable: false),
                    QualityProcess = table.Column<string>(maxLength: 500, nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    Validity = table.Column<string>(maxLength: 50, nullable: false),
                    QuoteType = table.Column<string>(maxLength: 50, nullable: false),
                    PaymentType = table.Column<string>(maxLength: 50, nullable: false),
                    Warranty = table.Column<string>(maxLength: 500, nullable: false),
                    DeliveryTime = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQuotes_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerQuotes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerQuotes_UserCores_UserId",
                        column: x => x.UserId,
                        principalTable: "UserCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerQuoteDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Consecutive = table.Column<int>(nullable: false),
                    TypeItem = table.Column<string>(maxLength: 150, nullable: false),
                    Unit = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Availability = table.Column<string>(maxLength: 250, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerQuoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuoteDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQuoteDetails_CustomerQuotes_CustomerQuoteId",
                        column: x => x.CustomerQuoteId,
                        principalTable: "CustomerQuotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocuments_CustomerId",
                table: "CustomerDocuments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuoteDetails_CustomerQuoteId",
                table: "CustomerQuoteDetails",
                column: "CustomerQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotes_CurrencyId",
                table: "CustomerQuotes",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotes_CustomerId",
                table: "CustomerQuotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotes_UserId",
                table: "CustomerQuotes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDocuments");

            migrationBuilder.DropTable(
                name: "CustomerQuoteDetails");

            migrationBuilder.DropTable(
                name: "CustomerQuotes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropColumn(
                name: "LiberationDate",
                table: "PurchaseOrders");
        }
    }
}
