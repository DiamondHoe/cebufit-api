using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRecipeService
{
    Task<RecipeDTO> CreateRecipeAsync(RecipeDTO recipe);
    Task<RecipeDTO> GetRecipeByIdAsync(Guid recipeId);
    Task<List<RecipeDTO>> GetAllRecipesAsync();
    Task UpdateRecipeAsync(RecipeDTO recipe);
    Task DeleteRecipeAsync(Guid recipeId);
}
