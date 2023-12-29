using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class StorageService : IStorageService
    {
        public Task<StorageDTO> CreateStorageAsync(StorageDTO storage)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStorageAsync(Guid storageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<StorageDTO>> GetAllStoragesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StorageDTO> GetStorageByIdAsync(Guid storageId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStorageAsync(StorageDTO storage)
        {
            throw new NotImplementedException();
        }
    }
}
