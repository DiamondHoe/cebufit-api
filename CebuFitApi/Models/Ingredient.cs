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
        public User? User { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public Product Product { get; set; } = new Product();
        public Recipe? Recipe { get; set; }
        public Meal? Meal { get; set; }
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
