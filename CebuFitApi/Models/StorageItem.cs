using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class StorageItem : BaseModel
    {
        public StorageItem()
        {

        }
        public User? User { get; set; }
        public DateTime DateOfPurchase { get; set; } = DateTime.UtcNow;
        public DateTime ExpirationDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public bool Eaten { get; set; }
        public Storage? Storage { get; set; }
        public Product Product { get; set; }
    }

    public class StorageItemConfiguration : IEntityTypeConfiguration<StorageItem>
    {
        public void Configure(EntityTypeBuilder<StorageItem> builder)
        {
            builder.HasOne(si => si.Product)
                   .WithMany(prod => prod.StorageItems);
        }
    }
}
