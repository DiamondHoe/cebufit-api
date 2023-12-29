using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IStorageRepository
{
    Task<List<Storage>> GetAllAsync();
    Task<Storage> GetByIdAsync(Guid id);
    Task CreateAsync(Storage storage);
    Task UpdateAsync(Storage storage);
    Task DeleteAsync(Guid id);
}
