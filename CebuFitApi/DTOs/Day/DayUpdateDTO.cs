using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class DayUpdateDTO
    {
        public Guid Id { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
    }
}
