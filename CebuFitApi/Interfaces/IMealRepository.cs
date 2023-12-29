using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IMealRepository
    {
        Task<List<Meal>> GetAllAsync();
        Task<Meal> GetByIdAsync(Guid id);
        Task CreateAsync(Meal blogPost);
        Task UpdateAsync(Meal blogPost);
        Task DeleteAsync(Guid id);
    }
}
