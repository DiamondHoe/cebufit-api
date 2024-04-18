namespace CebuFitApi.DTOs
{
    public class StorageItemWithProductDTO
    {
        public Guid Id { get; set; }
        public DateTime expirationDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? BoughtQuantity { get; set; }
        public decimal? BoughtWeight { get; set; }
        public decimal? ActualQuantity { get; set; }
        public decimal? ActualWeight { get; set; }
        public ProductWithDetailsDTO Product { get; set; }
    }
}
