using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IMealRepository
    {
        Task<List<Meal>> GetAllAsync();
        Task<List<Meal>> GetAllWithDetailsAsync();
        Task<Meal> GetByIdAsync(Guid mealId);
        Task<Meal> GetByIdWithDetailsAsync(Guid mealId);
        Task<Guid> CreateAsync(Meal meal);
        Task UpdateAsync(Meal meal);
        Task DeleteAsync(Guid mealId);
    }
}
