using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestPlace.Infrastructure.Migrations
{
    public partial class RemoveLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
