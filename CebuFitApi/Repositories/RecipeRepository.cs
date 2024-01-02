using CebuFitApi.Data;
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
        public async Task<List<Recipe>> GetAllAsync()
        {
            var recipes = await _dbContext.Recipes
                .Include(x => x.Ingredients)
                .ToListAsync();
            return recipes;
        }
        public async Task<List<Recipe>> GetAllWithDetailsAsync()
        {
            var recipes = await _dbContext.Recipes
                .Include(x => x.Ingredients)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .Include(x => x.Ingredients)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Macro)
                .ToListAsync();
            return recipes;
        }
        public async Task<Recipe> GetByIdAsync(Guid id)
        {
            var recipe = await _dbContext.Recipes
                .Include(x => x.Ingredients)
                .Where(rec => rec.Id == id)
                .FirstOrDefaultAsync();
            return recipe;
        }
        public async Task<Recipe> GetByIdWithDetailsAsync(Guid id)
        {
            var recipe = await _dbContext.Recipes
                .Include(x => x.Ingredients)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .Include(x => x.Ingredients)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Macro)
                .Where(rec => rec.Id == id)
                .FirstOrDefaultAsync();
            return recipe;
        }
        public async Task CreateAsync(Recipe recipe)
        {
            await _dbContext.Recipes.AddAsync(recipe);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Recipe recipe)
        {
            var existingRecipe = await _dbContext.Recipes
                .FirstOrDefaultAsync(si => si.Id == recipe.Id);

            if (existingRecipe != null)
            {
                _dbContext.Entry(existingRecipe).CurrentValues.SetValues(recipe);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            var recipeToDelete = await _dbContext.Recipes.FindAsync(id);
            if (recipeToDelete != null)
            {
                _dbContext.Recipes.Remove(recipeToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
