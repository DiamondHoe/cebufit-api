using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IStorageItemRepository
{
    Task<List<StorageItem>> GetAllAsync();
    Task<StorageItem> GetByIdAsync(Guid id);
    Task CreateAsync(StorageItem storageItem);
    Task UpdateAsync(StorageItem storageItem);
    Task DeleteAsync(Guid id);
}
