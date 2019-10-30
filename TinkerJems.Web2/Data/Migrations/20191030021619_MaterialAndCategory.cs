using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkerJems.Web2.Data.Migrations
{
    public partial class MaterialAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "JewelryItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "JewelryItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "JewelryItems");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "JewelryItems");
        }
    }
}
