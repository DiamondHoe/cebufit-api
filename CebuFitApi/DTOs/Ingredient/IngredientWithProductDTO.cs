namespace CebuFitApi.DTOs
{
    public class IngredientWithProductDTO
    {
        public Guid Id { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public ProductWithMacroDTO Product { get; set; }
    }
}
