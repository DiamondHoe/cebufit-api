using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class MealCreateDTO
    {
        public string? Name { get; set; }
        public bool Eaten { get; set; }
        public MealTimesEnum MealTime { get; set; }
        public List<Guid> IngredientsId { get; set; }
    }
}
