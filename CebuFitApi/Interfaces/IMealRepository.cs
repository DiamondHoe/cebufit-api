using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IMealRepository
    {
        Task<List<Meal>> GetAllAsync(Guid userIdClaim);
        Task<List<Meal>> GetAllWithDetailsAsync(Guid userIdClaim);
        Task<Meal> GetByIdAsync(Guid mealId, Guid userIdClaim);
        Task<Meal> GetByIdWithDetailsAsync(Guid mealId, Guid userIdClaim);
        Task<Guid> CreateAsync(Meal meal, Guid userIdClaim);
        Task UpdateAsync(Meal meal, Guid userIdClaim);
        Task DeleteAsync(Guid mealId, Guid userIdClaim);
    }
}
