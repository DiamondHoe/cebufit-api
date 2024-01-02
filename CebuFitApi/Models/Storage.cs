using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Storage : BaseModel
    {
        public Storage(Guid id)
        {
            Id = id;
        }
        public List<StorageItem>? StorageItems { get; set; }
    }
    public class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.HasMany(stor => stor.StorageItems)
                .WithOne(storI => storI.Storage);
        }
    }
}
