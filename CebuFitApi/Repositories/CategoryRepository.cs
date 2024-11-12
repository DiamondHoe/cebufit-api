using CebuFitApi.Data;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public CategoryRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Category>> GetAllAsync(Guid userIdClaim)
        {
            var categories = await _dbContext.Categories
                .Where(x => x.User.Id == userIdClaim)
                .ToListAsync();
            return categories;
        }
        public async Task<Category> GetByIdAsync(Guid categoryId, Guid userIdClaim)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId && x.User.Id == userIdClaim);
            return category;
        }
        public async Task AddAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category, Guid userIdClaim)
        {
            var existingCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == category.Id && x.User.Id == userIdClaim);
            if (existingCategory != null)
            {
                _dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid categoryId, Guid userIdClaim)
        {
            var categoryToDelete = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId && x.User.Id == userIdClaim);
            if (categoryToDelete != null)
            {
                var productsToUpdate = _dbContext.Products
                    .Where(p => p.Category.Id == categoryId)
                    .ToList();
                foreach (var product in productsToUpdate)
                {
                    product.Category = null;
                }
                _dbContext.Categories.Remove(categoryToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
