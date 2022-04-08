using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestPlace.Infrastructure.Migrations
{
    public partial class UpdateQuestionnaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Questionnaires",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Questionnaires");
        }
    }
}
