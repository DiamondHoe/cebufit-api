using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CebuFitApi.Migrations
{
    /// <inheritdoc />
    public partial class RelationsCd6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Day_DayId",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Recipe_RecipeId",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageItem_Storage_StorageId",
                table: "StorageItem");

            migrationBuilder.DropIndex(
                name: "IX_StorageItem_StorageId",
                table: "StorageItem");

            migrationBuilder.DropIndex(
                name: "IX_Meal_DayId",
                table: "Meal");

            migrationBuilder.DropIndex(
                name: "IX_Meal_RecipeId",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "StorageItem");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Meal");

            migrationBuilder.AlterColumn<decimal>(
                name: "Sugar",
                table: "Macro",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "SaturatedFattyAcid",
                table: "Macro",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Salt",
                table: "Macro",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Protein",
                table: "Macro",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Fat",
                table: "Macro",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Carb",
                table: "Macro",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Day_Id",
                table: "Meal",
                column: "Id",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Recipe_Id",
                table: "Meal",
                column: "Id",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItem_Storage_Id",
                table: "StorageItem",
                column: "Id",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Day_Id",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Recipe_Id",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageItem_Storage_Id",
                table: "StorageItem");

            migrationBuilder.AddColumn<Guid>(
                name: "StorageId",
                table: "StorageItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DayId",
                table: "Meal",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeId",
                table: "Meal",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Sugar",
                table: "Macro",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SaturatedFattyAcid",
                table: "Macro",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salt",
                table: "Macro",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Protein",
                table: "Macro",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Fat",
                table: "Macro",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Carb",
                table: "Macro",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageItem_StorageId",
                table: "StorageItem",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_DayId",
                table: "Meal",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_RecipeId",
                table: "Meal",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Day_DayId",
                table: "Meal",
                column: "DayId",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Recipe_RecipeId",
                table: "Meal",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItem_Storage_StorageId",
                table: "StorageItem",
                column: "StorageId",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
