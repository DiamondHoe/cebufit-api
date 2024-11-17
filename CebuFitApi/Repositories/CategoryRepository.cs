using CebuFitApi.Data;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class CategoryRepository(CebuFitApiDbContext dbContext) : ICategoryRepository
    {
        public async Task<List<Category>> GetAllAsync(Guid userIdClaim, DataType dataType)
        {
            return dataType switch
            {
                DataType.Private => await dbContext.Categories
                    .Where(x => x.User != null && x.User.Id == userIdClaim && x.IsPublic == false)
                    .ToListAsync(),
                DataType.Public => await dbContext.Categories
                    .Where(x => x.IsPublic == true)
                    .ToListAsync(),
                DataType.Both => await dbContext.Categories
                    .Where(x => x.User != null && x.User.Id == userIdClaim || x.IsPublic == true)
                    .ToListAsync(),
                _ => await dbContext.Categories
                    .Where(x => x.User != null && x.User.Id == userIdClaim)
                    .ToListAsync(),
            };
        }
        public async Task<Category?> GetByIdAsync(Guid categoryId, Guid userIdClaim)
        {
            var category = await dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId);
            return category;
        }
        public async Task AddAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category, Guid userIdClaim)
        {
            var existingCategory = await dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == category.Id && x.User.Id == userIdClaim);
            if (existingCategory != null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid categoryId, Guid userIdClaim)
        {
            var categoryToDelete = await dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId && x.User.Id == userIdClaim);
            if (categoryToDelete != null)
            {
                var productsToUpdate = dbContext.Products
                    .Where(p => p.Category.Id == categoryId)
                    .ToList();
                foreach (var product in productsToUpdate)
                {
                    product.Category = null;
                }
                dbContext.Categories.Remove(categoryToDelete);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
