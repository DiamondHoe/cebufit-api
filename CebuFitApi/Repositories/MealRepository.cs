using CebuFitApi.Data;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CebuFitApi.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public MealRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Meal>> GetAllAsync(Guid userIdClaim)
        {
            var meals = await _dbContext.Meals
                .Where(x => x.User.Id == userIdClaim)
                .Include(m => m.Ingredients)
                .ToListAsync();
            return meals;
        }

        public async Task<List<Meal>> GetAllWithDetailsAsync(Guid userIdClaim)
        {
            var meals = await _dbContext.Meals
                .Where(x => x.User.Id == userIdClaim)
                .Include(m => m.Ingredients)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Category)
                .Include(m => m.Ingredients)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Macro)
                .ToListAsync();
            return meals;
        }

        public async Task<Meal> GetByIdAsync(Guid mealId, Guid userIdClaim)
        {
            var meals = await _dbContext.Meals
                .Include(m => m.Ingredients)
                .Where(m => m.Id == mealId && m.User.Id == userIdClaim)
                .FirstAsync();
            return meals;
        }

        public async Task<Meal> GetByIdWithDetailsAsync(Guid mealId, Guid userIdClaim)
        {
            var meals = await _dbContext.Meals
                .Include(m => m.Ingredients)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Category)
                .Include(m => m.Ingredients)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Macro)
                .Where(m => m.Id == mealId)
                .Where(x => x.User.Id == userIdClaim)
                .FirstOrDefaultAsync();
            return meals;
        }
        public async Task<Guid> CreateAsync(Meal meal, Guid userIdClaim)
        {
            await _dbContext.Meals.AddAsync(meal);
            await _dbContext.SaveChangesAsync();
            return meal.Id;
        }

        public async Task UpdateAsync(Meal meal, Guid userIdClaim)
        {
            var existingMeal = await _dbContext.Meals
                 .Where(x => x.User.Id == userIdClaim)
                .Include(x => x.Ingredients)
                .FirstOrDefaultAsync(m => m.Id == meal.Id);

            if (existingMeal != null)
            {
                _dbContext.Entry(existingMeal).CurrentValues.SetValues(meal);
                existingMeal.Ingredients = meal.Ingredients;
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(Guid mealId, Guid userIdClaim)
        {
            var mealToDelete = await _dbContext.Meals
                .Where(x => x.Id == mealId && x.User.Id == userIdClaim)
                .FirstAsync();
            if (mealToDelete != null)
            {
                _dbContext.Meals.Remove(mealToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
