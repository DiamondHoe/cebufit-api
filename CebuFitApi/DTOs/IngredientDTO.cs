using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class IngredientDTO
    {
        public Guid Id { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
    }
}
