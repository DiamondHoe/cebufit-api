namespace CebuFitApi.DTOs
{
    public class StorageItemCreateDTO
    {
        public DateTime expirationDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public Guid baseProductId { get; set; }
    }
}
