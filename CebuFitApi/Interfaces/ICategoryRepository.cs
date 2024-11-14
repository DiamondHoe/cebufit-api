using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(Guid userIdClaim, DataType dataType);
        Task<Category?> GetByIdAsync(Guid categoryId, Guid userIdClaim);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category, Guid userIdClaim);
        Task DeleteAsync(Guid categoryId, Guid userIdClaim);
    }
}
