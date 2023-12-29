using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Meal
    {
        public Meal()
        {
            
        }

        [Key]
        public Guid Id { get; set; }
        public string? Name {  get; set; }
        public bool Eaten { get; set; }
        public bool Doable { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
        public Recipe Recipe { get; set; }
        public Day Day { get; set; }
    }
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasMany(meal => meal.Ingredients)
                .WithMany(ing => ing.Meals);

            builder.HasOne(meal => meal.Recipe)
                .WithMany(rec => rec.Meals)
                .HasForeignKey(meal => meal.Id);
        }
    }
}
