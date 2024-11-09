using CebuFitApi.Helpers.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Product : BaseModel
    {
        public Product()
        {
         
        }
        public User? User { get; set; }
        public string? Name { get; set; }
        public ImportanceEnum Importance { get; set; }
        public bool IsPublic { get; set; }
        public bool Packaged { get; set; }
        public int UnitWeight { get; set; }
        public ProductType ProductType { get; set; }
        public Category? Category { get; set; }
        public Macro? Macro { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<StorageItem> StorageItems { get; set; } = new List<StorageItem>();

    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));
        }
    }
}
