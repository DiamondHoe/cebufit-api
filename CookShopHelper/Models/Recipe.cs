namespace CebuFitApi.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public IEnumerable<Ingredient>? Ingredients { get; set; }
    }
}
