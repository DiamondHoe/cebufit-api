using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRecipeService
{
    Task<List<RecipeDTO>> GetAllRecipesAsync();
    Task<List<RecipeWithDetailsDTO>> GetAllRecipesWithDetailsAsync();
    Task<RecipeDTO> GetRecipeByIdAsync(Guid recipeId);
    Task<RecipeWithDetailsDTO> GetRecipeByIdWithDetailsAsync(Guid recipeId);
    Task CreateRecipeAsync(RecipeCreateDTO recipe);
    Task UpdateRecipeAsync(RecipeUpdateDTO recipe);
    Task DeleteRecipeAsync(Guid recipeId);
}
