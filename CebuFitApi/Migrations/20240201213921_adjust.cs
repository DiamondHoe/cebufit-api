using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CebuFitApi.Migrations
{
    /// <inheritdoc />
    public partial class adjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "expirationDate",
                table: "StorageItems",
                newName: "ExpirationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPurchase",
                table: "StorageItems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Eaten",
                table: "StorageItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Prepared",
                table: "Meals",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfPurchase",
                table: "StorageItems");

            migrationBuilder.DropColumn(
                name: "Eaten",
                table: "StorageItems");

            migrationBuilder.DropColumn(
                name: "Prepared",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "StorageItems",
                newName: "expirationDate");
        }
    }
}
