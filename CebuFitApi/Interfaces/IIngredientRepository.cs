using CebuFitApi.Models;

public interface IIngredientRepository
{
    Task<List<Ingredient>> GetAllAsync(Guid userIdClaim);
    Task<List<Ingredient>> GetAllWithProductAsync(Guid userIdClaim);
    Task<Ingredient> GetByIdAsync(Guid id, Guid userIdClaim);
    Task<Ingredient> GetByIdWithProductAsync(Guid id, Guid userIdClaim);
    Task CreateAsync(Ingredient ingredient, Guid userIdClaim);
    Task UpdateAsync(Ingredient ingredient, Guid userIdClaim);
    Task DeleteAsync(Guid id, Guid userIdClaim);
}
