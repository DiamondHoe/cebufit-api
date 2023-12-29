using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class StorageItemService : IStorageItemService
    {
        public Task<StorageItemDTO> CreateStorageItemAsync(StorageItemDTO storageItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStorageItemAsync(Guid storageItemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<StorageItemDTO>> GetAllStorageItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StorageItemDTO> GetStorageItemByIdAsync(Guid storageItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStorageItemAsync(StorageItemDTO storageItem)
        {
            throw new NotImplementedException();
        }
    }
}
