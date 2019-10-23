using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkerJems.Web2.Data.Migrations
{
    public partial class slugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "TinkerJemsBlogPost",
                nullable: false,
                defaultValue: "Slug");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "TinkerJemsBlogPost");
        }
    }
}
