using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Storage
    {
        [Key]
        public Guid Id { get; set; }
        public List<StorageItem>? StorageItems { get; set; }
    }
    public class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.HasMany(stor => stor.StorageItems)
                .WithOne(storI => storI.Storage)
                .HasForeignKey(storI => storI.Id);
        }
    }
}
