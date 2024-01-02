using CebuFitApi.Helpers.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Product : BaseModel
    {
        public string? Name { get; set; }
        public ImportanceEnum Importance { get; set; }
        public int UnitWeight { get; set; }
        public Category? Category { get; set; }
        public Macro? Macro { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<StorageItem> StorageItems { get; set; } = new();

    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));
        }
    }
}
