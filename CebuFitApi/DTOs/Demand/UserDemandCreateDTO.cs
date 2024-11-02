namespace CebuFitApi.DTOs.Demand
{
    public class UserDemandCreateDTO
    {
        public int Calories { get; set; }
        public int CarbPercent { get; set; } = 30;
        public int FatPercent { get; set; } = 30;
        public int ProteinPercent { get; set; } = 40;
    }
}
