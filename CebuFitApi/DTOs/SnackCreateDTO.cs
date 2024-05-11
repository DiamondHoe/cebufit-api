using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class SnackCreateDTO
    {
        public string? Name { get; set; }
        public IngredientCreateDTO Ingredient { get; set; }
        public StorageItemPrepareDTO StorageItem { get; set; }
        public Guid DayId { get; set; }
    }
}
