using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Category
    {
        public Category()
        {

        }

        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Product> Products { get; set; }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(cat => cat.Products)
                   .WithOne(prod => prod.Category)
                   .HasForeignKey(prod => prod.Id);
        }
    }
}
