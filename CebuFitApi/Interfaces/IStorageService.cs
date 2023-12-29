using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IStorageService
{
    Task<StorageDTO> CreateStorageAsync(StorageDTO storage);
    Task<StorageDTO> GetStorageByIdAsync(Guid storageId);
    Task<List<StorageDTO>> GetAllStoragesAsync();
    Task UpdateStorageAsync(StorageDTO storage);
    Task DeleteStorageAsync(Guid storageId);
}
