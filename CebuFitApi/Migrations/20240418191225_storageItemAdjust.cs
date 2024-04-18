using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CebuFitApi.Migrations
{
    /// <inheritdoc />
    public partial class storageItemAdjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prepared",
                table: "StorageItems");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "StorageItems",
                newName: "BoughtWeight");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "StorageItems",
                newName: "BoughtQuantity");

            migrationBuilder.AddColumn<decimal>(
                name: "ActualQuantity",
                table: "StorageItems",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualWeight",
                table: "StorageItems",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualQuantity",
                table: "StorageItems");

            migrationBuilder.DropColumn(
                name: "ActualWeight",
                table: "StorageItems");

            migrationBuilder.RenameColumn(
                name: "BoughtWeight",
                table: "StorageItems",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "BoughtQuantity",
                table: "StorageItems",
                newName: "Quantity");

            migrationBuilder.AddColumn<bool>(
                name: "Prepared",
                table: "StorageItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
