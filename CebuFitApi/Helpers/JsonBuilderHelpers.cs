using CebuFitApi.DTOs;

namespace CebuFitApi.Helpers
{
    public class RecipeDetail
    {
        public RecipeWithDetailsDTO Recipe { get; set; }
        public List<IngredientDetail> Ingredients { get; set; }
    }
    public class IngredientDetail
    {
        public IngredientWithProductDTO Ingredient { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
    }
}
