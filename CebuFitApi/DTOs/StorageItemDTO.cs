using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class StorageItemDTO
    {
        public Guid Id { get; set; }
        public DateTime expirationDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
    }
}
