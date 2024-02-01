using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.Models
{
    public class Meal : BaseModel
    {
        public Meal()
        {
            
        }
        public User? User { get; set; }
        public string Name {  get; set; }
        public bool Eaten { get; set; }
        public bool Prepared { get; set; }
        public MealTimesEnum MealTime { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
        public Day? Day { get; set; }
    }
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasMany(meal => meal.Ingredients)
                .WithOne(ing => ing.Meal);
        }
    }
}
