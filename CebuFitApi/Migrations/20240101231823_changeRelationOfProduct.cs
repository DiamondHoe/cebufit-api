using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CebuFitApi.Migrations
{
    /// <inheritdoc />
    public partial class changeRelationOfProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Product_Id",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientMeal_Ingredient_IngredientsId",
                table: "IngredientMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Ingredient_IngredientsId",
                table: "IngredientRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageItem_Product_Id",
                table: "StorageItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageItem_Storages_Id",
                table: "StorageItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageItem",
                table: "StorageItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "StorageItem",
                newName: "StorageItems");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageItems",
                table: "StorageItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientMeal_Ingredients_IngredientsId",
                table: "IngredientMeal",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Ingredients_IngredientsId",
                table: "IngredientRecipe",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Product_Id",
                table: "Ingredients",
                column: "Id",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItems_Product_Id",
                table: "StorageItems",
                column: "Id",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItems_Storages_Id",
                table: "StorageItems",
                column: "Id",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientMeal_Ingredients_IngredientsId",
                table: "IngredientMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Ingredients_IngredientsId",
                table: "IngredientRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Product_Id",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageItems_Product_Id",
                table: "StorageItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageItems_Storages_Id",
                table: "StorageItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageItems",
                table: "StorageItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "StorageItems",
                newName: "StorageItem");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageItem",
                table: "StorageItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Product_Id",
                table: "Ingredient",
                column: "Id",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientMeal_Ingredient_IngredientsId",
                table: "IngredientMeal",
                column: "IngredientsId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Ingredient_IngredientsId",
                table: "IngredientRecipe",
                column: "IngredientsId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItem_Product_Id",
                table: "StorageItem",
                column: "Id",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageItem_Storages_Id",
                table: "StorageItem",
                column: "Id",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
