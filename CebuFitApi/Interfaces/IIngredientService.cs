using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IIngredientService
{
    Task<IngredientDTO> CreateIngredientAsync(IngredientDTO ingredient);
    Task<IngredientDTO> GetIngredientByIdAsync(Guid ingredientId);
    Task<List<IngredientDTO>> GetAllIngredientsAsync();
    Task UpdateIngredientAsync(IngredientDTO ingredient);
    Task DeleteIngredientAsync(Guid ingredientId);
}
