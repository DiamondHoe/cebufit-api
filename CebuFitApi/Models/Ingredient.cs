namespace CebuFitApi.Models
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public Product AssociatedProduct { get; set; }
    }
}
