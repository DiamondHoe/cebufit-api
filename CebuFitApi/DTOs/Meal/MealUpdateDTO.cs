using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class MealUpdateDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool Eaten { get; set; }
        public MealTimesEnum MealTime { get; set; }
        public List<IngredientCreateDTO> Ingredients { get; set; }
    }
}
