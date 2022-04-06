using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestPlace.Infrastructure.Migrations
{
    public partial class RemoveIsboughtOnItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBought",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBought",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
