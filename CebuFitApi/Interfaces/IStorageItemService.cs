using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
