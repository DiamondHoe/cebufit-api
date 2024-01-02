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
        public async Task<List<Ingredient>> GetAllAsync()
        {
            var ingredients = await _dbContext.Ingredients.ToListAsync();
            return ingredients;
        }
        public async Task<List<Ingredient>> GetAllWithProductAsync()
        {
            var ingredients = await _dbContext.Ingredients
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .Include(x => x.Product)
                .ThenInclude(x => x.Macro)
                .ToListAsync();
            return ingredients;
        }
        public async Task<Ingredient> GetByIdAsync(Guid id)
        {
            var ingredient = await _dbContext.Ingredients
                 .Where(ing => ing.Id == id)
                 .FirstOrDefaultAsync();
            return ingredient;
        }
        public async Task<Ingredient> GetByIdWithProductAsync(Guid id)
        {
            var ingredient = await _dbContext.Ingredients
                    .Include(x => x.Product)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.Product)
                    .ThenInclude(x => x.Macro)
                    .FirstOrDefaultAsync();
            return ingredient;

        }
        public async Task CreateAsync(Ingredient ingredient)
        {
            await _dbContext.Ingredients.AddAsync(ingredient);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Ingredient ingredient)
        {
            var existingIngredient = await _dbContext.Ingredients
                .FirstOrDefaultAsync(ing => ing.Id == ingredient.Id);

            if (existingIngredient != null)
            {
                _dbContext.Entry(existingIngredient).CurrentValues.SetValues(ingredient);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            var IngredientToDelete = await _dbContext.StorageItems.FindAsync(id);
            if (IngredientToDelete != null)
            {
                _dbContext.StorageItems.Remove(IngredientToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
