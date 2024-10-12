using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CebuFitApi.Models
{
    public class Demand : BaseModel
    {
        public Demand()
        {

        }
        // ROBSON: Pamiętaj o tym żeby na froncie zabezpieczyć żeby procenty Carbs, Fat i Protein były zawsze sumą 100
        public int? Calories { get; set; }

        public int? CarbPercent { get; set; } = 30;
        public decimal? Carb => Calories * CarbPercent / 100 / 4;

        public int? FatPercent { get; set; } = 30;
        public decimal? Fat => Calories * FatPercent / 100 / 9;

        public int? ProteinPercent { get; set; } = 40;
        public decimal? Protein => Calories * ProteinPercent / 100 / 4;

        public User? User { get; set; }
        public Guid UserId { get; set; }
    }
    public class DemandConfiguration : IEntityTypeConfiguration<Demand>
    {
        public void Configure(EntityTypeBuilder<Demand> builder)
        {
            builder.HasOne(demand => demand.User)
                .WithOne(user => user.Demand)
                .HasForeignKey<Demand>(demand => demand.UserId);
        }
    }
}
