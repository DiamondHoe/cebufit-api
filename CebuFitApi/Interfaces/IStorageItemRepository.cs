using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IStorageItemRepository
{
    Task<List<StorageItem>> GetAllAsync();
    Task<List<StorageItem>> GetAllWithProductAsync();
    Task<StorageItem> GetByIdAsync(Guid id);
    Task<StorageItem> GetByIdWithProductAsync(Guid id);
    Task CreateAsync(StorageItem storageItem);
    Task UpdateAsync(StorageItem storageItem);
    Task DeleteAsync(Guid id);
}
