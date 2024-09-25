namespace CebuFitApi.DTOs
{
    public class MealPrepareDTO
    {
        public Guid Id { get; set; }
        public List<StorageItemPrepareDTO> StorageItems { get; set; }
    }
}
