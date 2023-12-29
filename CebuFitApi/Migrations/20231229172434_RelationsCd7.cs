using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CebuFitApi.Migrations
{
    /// <inheritdoc />
    public partial class RelationsCd7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogueRecipe_Catalogue_CataloguesId",
                table: "CatalogueRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_CatalogueRecipe_Recipe_RecipesId",
                table: "CatalogueRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientMeal_Meal_MealsId",
                table: "IngredientMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Recipe_RecipesId",
                table: "IngredientRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Macro_Product_ProductId",
                table: "Macro");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Day_Id",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Recipe_Id",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageItem_Storage_Id",
                table: "StorageItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storage",
                table: "Storage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meal",
                table: "Meal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Macro",
                table: "Macro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Day",
                table: "Day");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalogue",
                table: "Catalogue");

            migrationBuilder.RenameTable(
                name: "Storage",
                newName: "Storages");

            migrationBuilder.RenameTable(
                name: "Recipe",
                newName: "Recipes");

            migrationBuilder.RenameTable(
                name: "Meal",
                newName: "Meals");

            migrationBuilder.RenameTable(
                name: "Macro",
                newName: "Macros");

            migrationBuilder.RenameTable(
                name: "Day",
                newName: "Days");

            migrationBuilder.RenameTable(
                name: "Catalogue",
                newName: "Catalogues");

            migrationBuilder.RenameIndex(
                name: "IX_Macro_ProductId",
                table: "Macros",
                newName: "IX_Macros_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storages",
                table: "Storages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meals",
                table: "Meals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Macros",
                table: "Macros",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Days",
                table: "Days",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalogues",
                table: "Catalogues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogueRecipe_Catalogues_CataloguesId",
                table: "CatalogueRecipe",
                column: "CataloguesId",
                principalTable: "Catalogues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogueRecipe_Recipes_RecipesId",
                table: "CatalogueRecipe",
                column: "RecipesId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientMeal_Meals_MealsId",
                table: "IngredientMeal",
                column: "MealsId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Recipes_RecipesId",
                table: "IngredientRecipe",
                column: "RecipesId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Macros_Product_ProductId",
                table: "Macros",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Days_Id",
                table: "Meals",
                column: "Id",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Recipes_Id",
                table: "Meals",
                column: "Id",
                principalTable: "Recipes",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogueRecipe_Catalogues_CataloguesId",
                table: "CatalogueRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_CatalogueRecipe_Recipes_RecipesId",
                table: "CatalogueRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientMeal_Meals_MealsId",
                table: "IngredientMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Recipes_RecipesId",
                table: "IngredientRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Macros_Product_ProductId",
                table: "Macros");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Days_Id",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Recipes_Id",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageItem_Storages_Id",
                table: "StorageItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storages",
                table: "Storages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meals",
                table: "Meals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Macros",
                table: "Macros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Days",
                table: "Days");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalogues",
                table: "Catalogues");

            migrationBuilder.RenameTable(
                name: "Storages",
                newName: "Storage");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "Recipe");

            migrationBuilder.RenameTable(
                name: "Meals",
                newName: "Meal");

            migrationBuilder.RenameTable(
                name: "Macros",
                newName: "Macro");

            migrationBuilder.RenameTable(
                name: "Days",
                newName: "Day");

            migrationBuilder.RenameTable(
                name: "Catalogues",
                newName: "Catalogue");

            migrationBuilder.RenameIndex(
                name: "IX_Macros_ProductId",
                table: "Macro",
                newName: "IX_Macro_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storage",
                table: "Storage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meal",
                table: "Meal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Macro",
                table: "Macro",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Day",
                table: "Day",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalogue",
                table: "Catalogue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogueRecipe_Catalogue_CataloguesId",
                table: "CatalogueRecipe",
                column: "CataloguesId",
                principalTable: "Catalogue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogueRecipe_Recipe_RecipesId",
                table: "CatalogueRecipe",
                column: "RecipesId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientMeal_Meal_MealsId",
                table: "IngredientMeal",
                column: "MealsId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Recipe_RecipesId",
                table: "IngredientRecipe",
                column: "RecipesId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Macro_Product_ProductId",
                table: "Macro",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
