using CebuFitApi.Data;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public ProductTypeRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ProductType>> GetAllAsync(Guid userIdClaim)
        {
            var productTypes = new List<ProductType>();
            // var productTypes = await _dbContext.Categories
                // .Where(x => x.User.Id == userIdClaim)
                // .ToListAsync();
            return productTypes;
        }
        public async Task<ProductType> GetByIdAsync(Guid categoryId, Guid userIdClaim)
        {
            var productType = new ProductType();
            // var category = await _dbContext.Categories
                // .Where(x => x.Id == categoryId && x.User.Id == userIdClaim)
                // .FirstOrDefaultAsync();
            return productType;
        }
        public async Task AddAsync(ProductType productType)
        {
            // await _dbContext.Categories.AddAsync(productType);
            // await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(ProductType productType, Guid userIdClaim)
        {
            var existingCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == productType.Id && x.User.Id == userIdClaim);
            if (existingCategory != null)
            {
                _dbContext.Entry(existingCategory).CurrentValues.SetValues(productType);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid categoryId, Guid userIdClaim)
        {
            var categoryToDelete = await _dbContext.Categories
                .Where(x => x.Id == categoryId && x.User.Id == userIdClaim)
                .FirstAsync();
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
