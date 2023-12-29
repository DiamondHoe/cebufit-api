using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class RecipeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
