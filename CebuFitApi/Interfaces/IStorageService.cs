using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IStorageService
{
    Task<StorageDTO> CreateStorageAsync(StorageDTO storage, Guid userIdClaim);
    Task<StorageDTO> GetStorageByIdAsync(Guid storageId, Guid userIdClaim);
    Task<List<StorageDTO>> GetAllStoragesAsync(Guid userIdClaim);
    Task UpdateStorageAsync(StorageDTO storage, Guid userIdClaim);
    Task DeleteStorageAsync(Guid storageId, Guid userIdClaim);
}
