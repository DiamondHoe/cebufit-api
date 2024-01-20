using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IIngredientService
{
    Task<List<IngredientDTO>> GetAllIngredientsAsync(Guid userIdClaim);
    Task<List<IngredientWithProductDTO>> GetAllIngredientsWithProductAsync(Guid userIdClaim);
    Task<IngredientDTO> GetIngredientByIdAsync(Guid ingredientId, Guid userIdClaim);
    Task<IngredientWithProductDTO> GetIngredientByIdWithProductAsync(Guid ingredientId, Guid userIdClaim);
    Task<Guid> CreateIngredientAsync(IngredientCreateDTO ingredient, Guid userIdClaim);
    Task UpdateIngredientAsync(IngredientDTO ingredient, Guid userIdClaim);
    Task DeleteIngredientAsync(Guid ingredientId, Guid userIdClaim);
    Task<bool> IsIngredientAvailable(IngredientCreateDTO ingredientDTO, Guid userIdClaim);
}
