namespace CebuFitApi.DTOs
{
    public class RecipeWithDetailsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<IngredientWithProductDTO> Ingredients { get; set; }
    }
}
