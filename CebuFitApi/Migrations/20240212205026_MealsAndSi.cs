using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CebuFitApi.Migrations
{
    /// <inheritdoc />
    public partial class MealsAndSi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MealId",
                table: "StorageItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageItems_MealId",
                table: "StorageItems",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItems_Meals_MealId",
                table: "StorageItems",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageItems_Meals_MealId",
                table: "StorageItems");

            migrationBuilder.DropIndex(
                name: "IX_StorageItems_MealId",
                table: "StorageItems");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "StorageItems");
        }
    }
}
