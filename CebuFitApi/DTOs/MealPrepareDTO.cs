﻿namespace CebuFitApi.DTOs
{
    public class MealPrepareDTO
    {
        public Guid Id { get; set; }
        public bool Prepared { get; set; }
        public List<StorageItemPrepare> StorageItems { get; set; }
    }
}