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
        public DateTime? DateOfPurchase { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? BoughtQuantity { get; set; }
        public decimal? BoughtWeight { get; set; }
        public decimal? ActualQuantity { get; set; }
        public decimal? ActualWeight { get; set; }
        public Product Product { get; set; } = new Product();
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
