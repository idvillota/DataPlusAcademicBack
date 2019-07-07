using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataPlus.Entities.Migrations
{
    public partial class updatePersonModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birth",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Teachers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birth",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Students",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birth",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Birth",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Students");
        }
    }
}
