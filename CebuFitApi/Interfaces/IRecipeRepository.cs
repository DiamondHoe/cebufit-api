using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetAllAsync();
    Task<List<Recipe>> GetAllWithDetailsAsync();
    Task<Recipe> GetByIdAsync(Guid id);
    Task<Recipe> GetByIdWithDetailsAsync(Guid id);
    Task CreateAsync(Recipe recipe);
    Task UpdateAsync(Recipe recipe);
    Task DeleteAsync(Guid id);
}
