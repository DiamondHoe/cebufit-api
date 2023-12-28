namespace CebuFitApi.Models
{
    public class Macro
    {
        public Guid Id { get; set; }
        public decimal Carb { get; set; }
        public decimal Sugar { get; set; }
        public decimal Fat { get; set; }
        public decimal SaturatedFattyAcid { get; set; }
        public decimal Protein { get; set; }
        public decimal Salt { get; set; }
    }
}
