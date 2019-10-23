using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkerJems.Web2.Data.Migrations
{
    public partial class SlugUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "Unique_Slug",
                table: "TinkerJemsBlogPost",
                column: "Slug");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "Unique_Slug",
                table: "TinkerJemsBlogPost");
        }
    }
}
