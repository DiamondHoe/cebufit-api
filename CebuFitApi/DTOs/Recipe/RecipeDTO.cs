using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class RecipeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public string Description { get; set; }
        public List<Guid> IngredientsId { get; set; }
    }
}
