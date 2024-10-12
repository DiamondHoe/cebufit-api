namespace CebuFitApi.DTOs.Demand
{
    public class DayDemandDTO
    {
        public int Calories { get; set; }
        public int CaloriesEaten { get; set; }

        public decimal Carb { get; set; }
        public decimal CarbEaten { get; set; }

        public decimal Fat { get; set; }
        public decimal FatEaten { get; set; }

        public decimal Protein { get; set; }
        public decimal ProteinEaten { get; set; }
    }
}
