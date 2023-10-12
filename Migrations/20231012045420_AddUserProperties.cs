using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunInVscode.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Age", "DateOfBirth", "Image", "Name", "Place" },
                values: new object[] { 33, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1.jpg", "Habeeb Ijaba", "SomePlace" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Age", "DateOfBirth", "Image", "Name", "Place" },
                values: new object[] { 32, new DateTime(1991, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2.jpg", "Safeer sfr", "AnotherPlace" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Age", "DateOfBirth", "Image", "Name", "Place" },
                values: new object[] { 31, new DateTime(1992, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "user3.jpg", "Saleel Hisan", "YetAnotherPlace" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Users");
        }
    }
}
