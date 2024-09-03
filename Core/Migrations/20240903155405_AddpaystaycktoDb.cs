using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddpaystaycktoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paystacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentHistoryId = table.Column<int>(type: "int", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    domain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gateway_response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    channel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ip_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exp_month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exp_year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    card_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reusable = table.Column<bool>(type: "bit", nullable: true),
                    signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    authorization_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    access_code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paystacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paystacks_Payments_PaymentHistoryId",
                        column: x => x.PaymentHistoryId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paystacks_PaymentHistoryId",
                table: "Paystacks",
                column: "PaymentHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paystacks");
        }
    }
}
