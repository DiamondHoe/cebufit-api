using CebuFitApi.DTOs;

public interface IStorageItemService
{
    Task<List<StorageItemDTO>> GetAllStorageItemsAsync();
    Task<List<StorageItemWithProductDTO>> GetAllStorageItemsWithProductAsync();
    Task<StorageItemDTO> GetStorageItemByIdAsync(Guid storageItemId);
    Task<StorageItemWithProductDTO> GetStorageItemByIdWithProductAsync(Guid storageItemId);
    Task CreateStorageItemAsync(StorageItemCreateDTO storageItem);
    Task UpdateStorageItemAsync(StorageItemDTO storageItem);
    Task DeleteStorageItemAsync(Guid storageItemId);
}
