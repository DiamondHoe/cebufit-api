using CebuFitApi.DTOs.Demand;

namespace CebuFitApi.DTOs
{
    public class DayDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<Guid> MealsId { get; set; } = new List<Guid>();
    }
}
