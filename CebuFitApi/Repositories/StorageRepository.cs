using CebuFitApi.Models;

namespace CebuFitApi.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        public Task CreateAsync(Storage storage)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Storage>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Storage> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Storage storage)
        {
            throw new NotImplementedException();
        }
    }
}
