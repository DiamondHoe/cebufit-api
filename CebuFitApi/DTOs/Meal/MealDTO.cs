using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class MealDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool Eaten { get; set; }
        public bool Prepared { get; set; }
        public bool Doable { get; set; }
        public MealTimesEnum MealTime { get; set; }
        public List<Guid> IngredientsId { get; set; }
    }
}