namespace CebuFitApi.DTOs
{
    public class StorageItemCreateDTO
    {
        public DateTime expirationDate { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public decimal? Price { get; set; }
        public decimal? BoughtQuantity { get; set; }
        public decimal? BoughtWeight { get; set; }
        public Guid baseProductId { get; set; }
    }
}
