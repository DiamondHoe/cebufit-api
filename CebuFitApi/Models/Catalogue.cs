using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Catalogue
    {
        [Key]
        public Guid Id { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
    public class CatalogueConfiguration : IEntityTypeConfiguration<Catalogue>
    {
        public void Configure(EntityTypeBuilder<Catalogue> builder)
        {
            builder.HasMany(cat => cat.Recipes)
                .WithMany(rec => rec.Catalogues);
        }
    }
}
