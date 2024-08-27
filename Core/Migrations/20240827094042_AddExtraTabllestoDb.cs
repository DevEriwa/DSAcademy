using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddExtraTabllestoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "CompanySettings",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnableCustomInvoice = table.Column<bool>(type: "bit", nullable: false),
                    CharLengthPerPage = table.Column<double>(type: "float", nullable: false),
                    DashboardUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableBasePackage = table.Column<bool>(type: "bit", nullable: false),
                    EnableSMS = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySettings", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanySettings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacebookAccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookAppId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookSecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoogleMapId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPriceMarkUp = table.Column<bool>(type: "bit", nullable: false),
                    PageSizeData = table.Column<int>(type: "int", nullable: true),
                    CompanyWorkHourPerDay = table.Column<int>(type: "int", nullable: true),
                    CompanyInvoiceTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsReceiptWaterMark = table.Column<bool>(type: "bit", nullable: false),
                    PriceMarkUp = table.Column<long>(type: "bigint", nullable: true),
                    QRScanDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QRScanLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReceiptColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptSubColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LunchStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    LunchEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    BusinessStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    BusinessEndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PageStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FullPageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageStatistics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PageStatistics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLoginLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLoginLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CompanyId",
                table: "Notifications",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PageStatistics_CompanyId",
                table: "PageStatistics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PageStatistics_UserId",
                table: "PageStatistics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginLogs_UserId",
                table: "UserLoginLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CompanySettings");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PageStatistics");

            migrationBuilder.DropTable(
                name: "UserLoginLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
