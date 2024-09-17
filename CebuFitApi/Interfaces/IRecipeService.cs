using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRecipeService
{
    Task<List<RecipeDTO>> GetAllRecipesAsync(Guid userIdClaim);
    Task<List<RecipeWithDetailsDTO>> GetAllRecipesWithDetailsAsync(Guid userIdClaim);
    Task<RecipeDTO> GetRecipeByIdAsync(Guid recipeId, Guid userIdClaim);
    Task<RecipeWithDetailsDTO> GetRecipeByIdWithDetailsAsync(Guid recipeId, Guid userIdClaim);
    Task CreateRecipeAsync(RecipeCreateDTO recipe, Guid userIdClaim);
    Task UpdateRecipeAsync(RecipeUpdateDTO recipe, Guid userIdClaim);
    Task DeleteRecipeAsync(Guid recipeId, Guid userIdClaim);
    Task<List<Tuple<RecipeWithDetailsDTO, List<Tuple<IngredientWithProductDTO, decimal?>>>>> GetRecipesFromAvailableStorageItemsAsync(Guid userIdClaim, int recipesCount);
}
