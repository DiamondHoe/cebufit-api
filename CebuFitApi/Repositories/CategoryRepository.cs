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
        public async Task<Category> AddAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task DeleteAsync(Guid categoryId)
        {
            var categoryToDelete = await _dbContext.Categories.FindAsync(categoryId);
            if (categoryToDelete != null)
            {
                _dbContext.Categories.Remove(categoryToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(Guid categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(category.Id);
            _dbContext.Entry(existingCategory).CurrentValues.SetValues(category);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();
        }
    }
}
