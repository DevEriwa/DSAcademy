using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddExtraDataToCompanySettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DarkMode",
                table: "CompanySettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FontFamily",
                table: "CompanySettings",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryColor",
                table: "CompanySettings",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryColor",
                table: "CompanySettings",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SidebarColor",
                table: "CompanySettings",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DarkMode",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "FontFamily",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "PrimaryColor",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "SecondaryColor",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "SidebarColor",
                table: "CompanySettings");
        }
    }
}
