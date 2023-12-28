using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICategoryService
{
    // Create a new category asynchronously
    Task<CategoryDTO> CreateCategoryAsync(Category category);

    // Read a category by ID asynchronously
    Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId);

    // Read all categories asynchronously
    Task<List<CategoryDTO>> GetAllCategoriesAsync();

    // Update an existing category asynchronously
    Task UpdateCategoryAsync(Category category);

    // Delete a category by ID asynchronously
    Task DeleteCategoryAsync(int categoryId);
}
