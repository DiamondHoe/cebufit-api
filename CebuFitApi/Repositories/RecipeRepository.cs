using CebuFitApi.Models;

namespace CebuFitApi.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        public Task CreateAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Recipe>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}
