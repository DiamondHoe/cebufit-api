using CebuFitApi.Models;

namespace CebuFitApi.Repositories
{
    public class StorageItemRepository : IStorageItemRepository
    {
        public Task CreateAsync(StorageItem storageItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<StorageItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StorageItem> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(StorageItem storageItem)
        {
            throw new NotImplementedException();
        }
    }
}
