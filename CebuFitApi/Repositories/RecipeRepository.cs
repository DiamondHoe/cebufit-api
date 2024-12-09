using CebuFitApi.Data;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public RecipeRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Recipe>> GetAllAsync(Guid userIdClaim)
        {
            var recipes = await _dbContext.Recipes
                .Where(x => x.User.Id == userIdClaim)
                .Include(x => x.Ingredients)
                .ToListAsync();
            return recipes;
        }
        public async Task<List<Recipe>> GetAllWithDetailsAsync(Guid userIdClaim, DataType dataType)
        {
            return dataType switch
            {
                DataType.Private => await _dbContext.Recipes
                    .Where(x => x.User.Id == userIdClaim && x.IsPublic == false)
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Macro)
                    .ToListAsync(),

                DataType.Public => await _dbContext.Recipes
                    .Where(x => x.IsPublic == true)
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Macro)
                    .ToListAsync(),

                DataType.Both => await _dbContext.Recipes
                    .Where(x => x.User.Id == userIdClaim || x.IsPublic == true)
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Macro)
                    .ToListAsync(),

                _ => await _dbContext.Recipes
                    .Where(x => x.User.Id == userIdClaim)
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Macro)
                    .ToListAsync(),
            };
        }
        public async Task<Recipe> GetByIdAsync(Guid id, Guid userIdClaim)
        {
            var recipe = await _dbContext.Recipes
                .Include(x => x.Ingredients)
                .FirstOrDefaultAsync(x => x.User.Id == userIdClaim && x.Id == id);
            return recipe;
        }
        public async Task<Recipe> GetByIdWithDetailsAsync(Guid id, Guid userIdClaim)
        {
            var recipe = await _dbContext.Recipes
                .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Macro)
                .FirstOrDefaultAsync(x => x.User.Id == userIdClaim && x.Id == id);
            return recipe;
        }
        public async Task CreateAsync(Recipe recipe, Guid userIdClaim)
        {
            await _dbContext.Recipes.AddAsync(recipe);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Recipe recipe, Guid userIdClaim)
        {
            var existingRecipe = await _dbContext.Recipes
                .Include (x => x.Ingredients)
                .FirstOrDefaultAsync(si => si.Id == recipe.Id && si.User.Id == userIdClaim);

            if (existingRecipe != null)
            {
                _dbContext.Entry(existingRecipe).CurrentValues.SetValues(recipe);
                existingRecipe.Ingredients = recipe.Ingredients;
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id, Guid userIdClaim)
        {
            var recipeToDelete = await _dbContext.Recipes
                .FirstOrDefaultAsync(x => x.Id == id && x.User.Id == userIdClaim);
            if (recipeToDelete != null)
            {
                _dbContext.Recipes.Remove(recipeToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
