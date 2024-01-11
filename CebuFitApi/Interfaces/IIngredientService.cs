using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IIngredientService
{
    Task<List<IngredientDTO>> GetAllIngredientsAsync();
    Task<List<IngredientWithProductDTO>> GetAllIngredientsWithProductAsync();
    Task<IngredientDTO> GetIngredientByIdAsync(Guid ingredientId);
    Task<IngredientWithProductDTO> GetIngredientByIdWithProductAsync(Guid ingredientId);
    Task<Guid> CreateIngredientAsync(IngredientCreateDTO ingredient);
    Task UpdateIngredientAsync(IngredientDTO ingredient);
    Task DeleteIngredientAsync(Guid ingredientId);
    Task<bool> IsIngredientAvailable(IngredientCreateDTO ingredientDTO);
}
