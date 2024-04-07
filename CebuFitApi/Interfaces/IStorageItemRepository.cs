using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IStorageItemRepository
    {
        Task<List<StorageItem>> GetAllAsync(Guid userIdClaim);
        Task<List<StorageItem>> GetAllWithProductAsync(Guid userIdClaim, bool withoutEaten);
        Task<List<StorageItem>> GetAllByProductIdWithProductAsync(Guid productId, Guid userIdClaim);
        Task<StorageItem> GetByIdAsync(Guid storageItemId, Guid userIdClaim);
        Task<StorageItem> GetByIdWithProductAsync(Guid storageItemId, Guid userIdClaim);
        Task CreateAsync(StorageItem storageItem, Guid userIdClaim);
        Task UpdateAsync(StorageItem storageItem, Guid userIdClaim);
        Task DeleteAsync(Guid storageItemId, Guid userIdClaim);
    }
}
