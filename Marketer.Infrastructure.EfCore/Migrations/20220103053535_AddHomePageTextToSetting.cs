using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketer.Infrastructure.EfCore.Migrations
{
    public partial class AddHomePageTextToSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SummaryText",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SummaryText",
                table: "Settings");
        }
    }
}
