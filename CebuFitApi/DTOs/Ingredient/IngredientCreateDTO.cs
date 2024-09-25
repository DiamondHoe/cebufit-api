namespace CebuFitApi.DTOs
{
    public class IngredientCreateDTO
    {
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public Guid baseProductId { get; set; }
    }
}
