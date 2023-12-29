using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class RecipeService : IRecipeService
    {
        public Task<RecipeDTO> CreateRecipeAsync(RecipeDTO recipe)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRecipeAsync(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<RecipeDTO>> GetAllRecipesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RecipeDTO> GetRecipeByIdAsync(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecipeAsync(RecipeDTO recipe)
        {
            throw new NotImplementedException();
        }
    }
}
