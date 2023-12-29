using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class DayDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<Meal> Meals { get; set; }
    }
}
