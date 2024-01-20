using CebuFitApi.Data;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public IngredientRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Ingredient>> GetAllAsync(Guid userIdClaim)
        {
            var ingredients = await _dbContext.Ingredients
                .Where(x => x.User.Id == userIdClaim)
                .ToListAsync();
            return ingredients;
        }
        public async Task<List<Ingredient>> GetAllWithProductAsync(Guid userIdClaim)
        {
            var ingredients = await _dbContext.Ingredients
                .Where(x => x.User.Id == userIdClaim)
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .Include(x => x.Product)
                .ThenInclude(x => x.Macro)
                .ToListAsync();
            return ingredients;
        }
        public async Task<Ingredient> GetByIdAsync(Guid id, Guid userIdClaim)
        {
            var ingredient = await _dbContext.Ingredients
                 .Where(x => x.User.Id == userIdClaim && x.Id == id)
                 .FirstAsync();
            return ingredient;
        }
        public async Task<Ingredient> GetByIdWithProductAsync(Guid id, Guid userIdClaim)
        {
            var ingredient = await _dbContext.Ingredients
                    .Where(x => x.User.Id == userIdClaim)
                    .Include(x => x.Product)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.Product)
                    .ThenInclude(x => x.Macro)
                    .FirstAsync();
            return ingredient;

        }
        public async Task CreateAsync(Ingredient ingredient, Guid userIdClaim)
        {
            await _dbContext.Ingredients.AddAsync(ingredient);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Ingredient ingredient, Guid userIdClaim)
        {
            var existingIngredient = await _dbContext.Ingredients
                .Where(x => x.User.Id == userIdClaim)
                .FirstAsync(ing => ing.Id == ingredient.Id);

            if (existingIngredient != null)
            {
                _dbContext.Entry(existingIngredient).CurrentValues.SetValues(ingredient);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id, Guid userIdClaim)
        {
            var IngredientToDelete = await _dbContext.Ingredients
                                .FirstAsync(x => x.User.Id == userIdClaim && x.Id == id);
            if (IngredientToDelete != null)
            {
                _dbContext.Ingredients.Remove(IngredientToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
