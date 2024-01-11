using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Ingredient : BaseModel
    {
        public Ingredient()
        {
            
        }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public Product Product { get; set; }
        public Recipe? Recipe { get; set; }
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
