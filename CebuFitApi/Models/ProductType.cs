using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CebuFitApi.Models;

public class ProductType: BaseModel
{
    public User? User { get; set; }
    public string Type { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new List<Product>();
}

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.HasMany(type => type.Products)
            .WithOne(prod => prod.ProductType);
    }
}