using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            
        }
        [Key]
        public Guid Id { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public Product Product { get; set; }
        public List<Recipe> Recipes { get; set; } = new();
        public List<Meal> Meals { get; set; } = new();
    }
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasOne(ing => ing.Product)
                .WithMany(prod => prod.Ingredients);
        }
    }
}
