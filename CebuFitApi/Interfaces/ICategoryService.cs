using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICategoryService
{
    Task<CategoryDTO> CreateCategoryAsync(CategoryCreateDTO category);
    Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId);
    Task<List<CategoryDTO>> GetAllCategoriesAsync();
    Task UpdateCategoryAsync(CategoryDTO category);
    Task DeleteCategoryAsync(Guid categoryId);
}
