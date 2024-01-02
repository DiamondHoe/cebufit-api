using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class StorageItem
    {
        public StorageItem()
        {

        }

        [Key]
        public Guid Id { get; set; }
        public DateTime expirationDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }

        //NP: for now default storage - in future storage per user
        public Storage Storage { get; set; } = new Storage(Guid.Empty);
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
