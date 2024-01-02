using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public class Macro : BaseModel
    {
        public int Calories { get; set; }
        public decimal? Carb { get; set; }
        public decimal? Sugar { get; set; }
        public decimal? Fat { get; set; }
        public decimal? SaturatedFattyAcid { get; set; }
        public decimal? Protein { get; set; }
        public decimal? Salt { get; set; }
        public Product Product { get; set; }
    }

    public class MacroConfiguration : IEntityTypeConfiguration<Macro>
    {
        public void Configure(EntityTypeBuilder<Macro> builder)
        {
            builder.HasOne(mac => mac.Product)
                   .WithOne(prod => prod.Macro)
                   .HasForeignKey<Macro>("ProductId");
        }
    }
}
