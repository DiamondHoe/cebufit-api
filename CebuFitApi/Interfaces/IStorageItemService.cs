using CebuFitApi.DTOs;

public interface IStorageItemService
{
    Task<List<StorageItemDTO>> GetAllStorageItemsAsync(Guid userIdClaim);
    Task<List<StorageItemWithProductDTO>> GetAllStorageItemsWithProductAsync(Guid userIdClaim);
    Task<List<StorageItemWithProductDTO>> GetAllStorageItemsByProductIdWithProductAsync(Guid productId, Guid userIdClaim);
    Task<StorageItemDTO> GetStorageItemByIdAsync(Guid storageItemId, Guid userIdClaim);
    Task<StorageItemWithProductDTO> GetStorageItemByIdWithProductAsync(Guid storageItemId, Guid userIdClaim);
    Task CreateStorageItemAsync(StorageItemCreateDTO storageItem, Guid userIdClaim);
    Task UpdateStorageItemAsync(StorageItemDTO storageItem, Guid userIdClaim);
    Task DeleteStorageItemAsync(Guid storageItemId, Guid userIdClaim);
}
