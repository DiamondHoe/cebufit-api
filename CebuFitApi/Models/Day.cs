using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Day
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<Meal>? Meals { get; set;}
    }
    public class DayConfiguration : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.HasMany(day => day.Meals)
                .WithOne(meal => meal.Day)
                .HasForeignKey(meal => meal.Id);
        }
    }
}
