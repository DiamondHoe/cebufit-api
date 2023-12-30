using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICategoryRepository
{
    Task<List<CategoryDTO>> GetAllCategoriesAsync();
    Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId);
    Task CreateCategoryAsync(CategoryCreateDTO category);
    Task UpdateCategoryAsync(CategoryDTO category);
    Task DeleteCategoryAsync(Guid categoryId);
}
