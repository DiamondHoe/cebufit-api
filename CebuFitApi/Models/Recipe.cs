using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Recipe : BaseModel
    {
        public Recipe()
        {
          
        }
        public User? User { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public string? Description { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<Meal> Meals { get; set; } = new List<Meal>();
    }

    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasMany(rec => rec.Ingredients)
                .WithOne(ing => ing.Recipe)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
