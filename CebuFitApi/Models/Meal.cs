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
        public string Name {  get; set; }
        public bool Eaten { get; set; }
        public MealTimesEnum MealTime { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
        public Day? Day { get; set; }
    }
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasMany(meal => meal.Ingredients)
                .WithMany(ing => ing.Meals);
        }
    }
}
