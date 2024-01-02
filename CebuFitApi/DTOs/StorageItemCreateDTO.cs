﻿namespace CebuFitApi.DTOs
{
    public class StorageItemCreateDTO
    {
        public Guid Id { get; set; }
        public DateTime expirationDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public Guid baseProductId { get; set; }
    }
}
