using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);
        Task<Category> GetByIdAsync(Guid categoryId);
        Task<List<Category>> GetAllAsync();
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid categoryId);
    }
}
