using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CebuFitApi.Migrations
{
    /// <inheritdoc />
    public partial class Users3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "StorageItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Recipes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Meals",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Ingredients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Days",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageItems_UserId",
                table: "StorageItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserId",
                table: "Meals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_UserId",
                table: "Ingredients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Days_UserId",
                table: "Days",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Users_UserId",
                table: "Days",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Users_UserId",
                table: "Ingredients",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Users_UserId",
                table: "Meals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItems_Users_UserId",
                table: "StorageItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Days_Users_UserId",
                table: "Days");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Users_UserId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Users_UserId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_UserId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageItems_Users_UserId",
                table: "StorageItems");

            migrationBuilder.DropIndex(
                name: "IX_StorageItems_UserId",
                table: "StorageItems");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Meals_UserId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_UserId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Days_UserId",
                table: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StorageItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");
        }
    }
}
