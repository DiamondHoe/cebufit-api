using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync(Guid userIdClaim);
        Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId, Guid userIdClaim);
        Task CreateCategoryAsync(CategoryCreateDTO categoryDTO, Guid userIdClaim);
        Task UpdateCategoryAsync(CategoryDTO categoryDTO, Guid userIdClaim);
        Task DeleteCategoryAsync(Guid categoryId, Guid userIdClaim);
    }
}
