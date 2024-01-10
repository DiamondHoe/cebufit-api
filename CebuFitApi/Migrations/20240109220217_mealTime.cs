using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CebuFitApi.Migrations
{
    /// <inheritdoc />
    public partial class mealTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MealTime",
                table: "Meals",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealTime",
                table: "Meals");
        }
    }
}
