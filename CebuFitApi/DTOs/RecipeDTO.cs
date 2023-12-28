using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class RecipeDTO
    {
        public Guid Id { get; set; }
        public IEnumerable<Ingredient>? Ingredients { get; set; }
    }
}
