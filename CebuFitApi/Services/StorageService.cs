using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class StorageService : IStorageService
    {
        public Task<StorageDTO> CreateStorageAsync(StorageDTO storage, Guid userIdClaim)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStorageAsync(Guid storageId, Guid userIdClaim)
        {
            throw new NotImplementedException();
        }

        public Task<List<StorageDTO>> GetAllStoragesAsync(Guid userIdClaim)
        {
            throw new NotImplementedException();
        }

        public Task<StorageDTO> GetStorageByIdAsync(Guid storageId, Guid userIdClaim)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStorageAsync(StorageDTO storage, Guid userIdClaim)
        {
            throw new NotImplementedException();
        }
    }
}
