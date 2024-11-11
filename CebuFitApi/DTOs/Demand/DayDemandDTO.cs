namespace CebuFitApi.DTOs.Demand
{
    public class DayDemandDTO
    {
        public int Calories { get; set; }
        public int CaloriesPlanned { get; set; }
        public int CaloriesEaten { get; set; }

        public decimal Carb { get; set; }
        public decimal CarbPlanned { get; set; }
        public decimal CarbEaten { get; set; }

        public decimal Fat { get; set; }
        public decimal FatPlanned { get; set; }
        public decimal FatEaten { get; set; }

        public decimal Protein { get; set; }
        public decimal ProteinPlanned { get; set; }
        public decimal ProteinEaten { get; set; }
    }
}
