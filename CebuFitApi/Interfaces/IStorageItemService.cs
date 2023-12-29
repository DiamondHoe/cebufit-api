using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IStorageItemService
{
    Task<StorageItemDTO> CreateStorageItemAsync(StorageItemDTO storageItem);
    Task<StorageItemDTO> GetStorageItemByIdAsync(Guid storageItemId);
    Task<List<StorageItemDTO>> GetAllStorageItemsAsync();
    Task UpdateStorageItemAsync(StorageItemDTO storageItem);
    Task DeleteStorageItemAsync(Guid storageItemId);
}
