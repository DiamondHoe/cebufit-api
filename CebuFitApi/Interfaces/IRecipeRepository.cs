using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetAllAsync(Guid userIdClaim);
    Task<List<Recipe>> GetAllWithDetailsAsync(Guid userIdClaim, DataType dataType);
    Task<Recipe?> GetByIdAsync(Guid id, Guid userIdClaim);
    Task<Recipe> GetByIdWithDetailsAsync(Guid id, Guid userIdClaim);
    Task CreateAsync(Recipe recipe, Guid userIdClaim);
    Task UpdateAsync(Recipe recipe, Guid userIdClaim);
    Task DeleteAsync(Guid id, Guid userIdClaim);
}
