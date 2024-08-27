using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
	public partial class AddPageStatisticsView : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
                CREATE VIEW View_PageStatisticsViews AS
                SELECT
                    ps.PageUrl,
                    COUNT(*) AS CountOfPage,
                    c.Name AS CompanyName,
                    c.Id AS CompanyId,
                    ps.PageName,
                    ps.RoleName
                FROM
                    dbo.PageStatistics AS ps
                INNER JOIN dbo.Companies AS c ON c.Id = ps.CompanyId
                GROUP BY
                    ps.PageUrl,
                    c.Id,
                    c.Name,
                    ps.PageName,
                    ps.RoleName;
            ");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP VIEW IF EXISTS View_PageStatisticsViews;");
		}
	}
}
