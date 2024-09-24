namespace CebuFitApi.DTOs
{
    public class RecipeUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<IngredientCreateDTO> Ingredients { get; set; }
    }
}
