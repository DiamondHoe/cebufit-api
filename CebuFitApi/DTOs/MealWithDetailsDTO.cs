using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class MealWithDetailsDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool Eaten { get; set; }
        public bool Doable { get; set; }
        public MealTimesEnum MealTime { get; set; }
        public List<IngredientWithProductDTO> Ingredients { get; set; }
    }
}
