using Microsoft.EntityFrameworkCore.Migrations;

namespace Mvc02.Data.Migrations
{
    public partial class migge5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FrontImage",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrontImage",
                table: "Product");
        }
    }
}
