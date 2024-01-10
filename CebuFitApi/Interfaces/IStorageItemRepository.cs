using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IStorageItemRepository
    {
        Task<List<StorageItem>> GetAllAsync();
        Task<List<StorageItem>> GetAllWithProductAsync();
        Task<StorageItem> GetByIdAsync(Guid storageItemId);
        Task<StorageItem> GetByIdWithProductAsync(Guid storageItemId);
        Task CreateAsync(StorageItem storageItem);
        Task UpdateAsync(StorageItem storageItem);
        Task DeleteAsync(Guid storageItemId);
    }
}
