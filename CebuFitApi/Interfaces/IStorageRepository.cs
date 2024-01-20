using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IStorageRepository
{
    Task<List<Storage>> GetAllAsync(Guid userIdClaim);
    Task<Storage> GetByIdAsync(Guid id, Guid userIdClaim);
    Task CreateAsync(Storage storage, Guid userIdClaim);
    Task UpdateAsync(Storage storage, Guid userIdClaim);
    Task DeleteAsync(Guid id, Guid userIdClaim);
}
