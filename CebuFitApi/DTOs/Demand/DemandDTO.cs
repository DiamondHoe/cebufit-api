namespace CebuFitApi.DTOs.Demand
{
    public class DemandDTO
    {
        public int? Calories { get; set; }

        public int? CarbPercent { get; set; } = 30;
        public decimal? Carb => Calories * CarbPercent / 100 / 4;

        public int? FatPercent { get; set; } = 30;
        public decimal? Fat => Calories * FatPercent / 100 / 9;

        public int? ProteinPercent { get; set; } = 40;
        public decimal? Protein => Calories * ProteinPercent / 100 / 4;
    }
}
