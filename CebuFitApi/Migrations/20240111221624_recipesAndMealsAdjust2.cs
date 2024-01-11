using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CebuFitApi.Migrations
{
    /// <inheritdoc />
    public partial class recipesAndMealsAdjust2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Product_ProductId",
                table: "Ingredients");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Ingredients",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Product_ProductId",
                table: "Ingredients",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Product_ProductId",
                table: "Ingredients");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Ingredients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Product_ProductId",
                table: "Ingredients",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
