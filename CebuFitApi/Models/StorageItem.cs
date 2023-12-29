using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class StorageItem : Product
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
        public Storage Storage { get; set; }
    }

    public class StorageItemConfiguration : IEntityTypeConfiguration<StorageItem>
    {
        public void Configure(EntityTypeBuilder<StorageItem> builder)
        {
            builder.ToTable(nameof(StorageItem));
        }
    }
}
