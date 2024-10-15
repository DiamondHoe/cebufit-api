using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Category : BaseModel
    {
        public Category()
        {

        }
        public User? User { get; set; }
        public string? Name { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(cat => cat.Products)
                   .WithOne(prod => prod.Category);
        }
    }
}
