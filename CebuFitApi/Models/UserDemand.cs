using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CebuFitApi.Models
{
    public class UserDemand : BaseModel
    {
        public UserDemand()
        {

        }
        // ROBSON: Pamiętaj o tym żeby na froncie zabezpieczyć żeby procenty Carbs, Fat i Protein były zawsze sumą 100
        public int? Calories { get; set; }

        public int? CarbPercent { get; set; } = 65;
        public decimal? Carb => Calories * CarbPercent / 100 / 4;

        public int? FatPercent { get; set; } = 20;
        public decimal? Fat => Calories * FatPercent / 100 / 9;

        public int? ProteinPercent { get; set; } = 15;
        public decimal? Protein => Calories * ProteinPercent / 100 / 4;

        public User? User { get; set; }
        public Guid UserId { get; set; }
    }
    public class UserDemandConfiguration : IEntityTypeConfiguration<UserDemand>
    {
        public void Configure(EntityTypeBuilder<UserDemand> builder)
        {
            builder.HasOne(demand => demand.User)
                .WithOne(user => user.Demand)
                .HasForeignKey<UserDemand>(demand => demand.UserId);
        }
    }
}
