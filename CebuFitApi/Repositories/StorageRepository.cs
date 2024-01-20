using CebuFitApi.Models;

namespace CebuFitApi.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        public Task CreateAsync(Storage storage, Guid userIdClaim)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, Guid userIdClaim)
        {
            throw new NotImplementedException();
        }

        public Task<List<Storage>> GetAllAsync(Guid userIdClaim)
        {
            throw new NotImplementedException();
        }

        public Task<Storage> GetByIdAsync(Guid id, Guid userIdClaim)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Storage storage, Guid userIdClaim)
        {
            throw new NotImplementedException();
        }
    }
}
