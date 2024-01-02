using CebuFitApi.Models;

public interface IIngredientRepository
{
    Task<List<Ingredient>> GetAllAsync();
    Task<List<Ingredient>> GetAllWithProductAsync();
    Task<Ingredient> GetByIdAsync(Guid id);
    Task<Ingredient> GetByIdWithProductAsync(Guid id);
    Task CreateAsync(Ingredient ingredient);
    Task UpdateAsync(Ingredient ingredient);
    Task DeleteAsync(Guid id);
}
