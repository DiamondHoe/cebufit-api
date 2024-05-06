namespace CebuFitApi.DTOs
{
    public class StorageItemCreateDTO
    {
        public DateTime? DateOfPurchase { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? BoughtQuantity { get; set; }
        public decimal? BoughtWeight { get; set; }
        public Guid BaseProductId { get; set; }
    }
}
