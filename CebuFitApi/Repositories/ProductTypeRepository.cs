using CebuFitApi.Data;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class ProductTypeRepository(CebuFitApiDbContext dbContext) : IProductTypeRepository
    {
        public async Task<List<ProductType>> GetAllAsync(Guid userIdClaim)
        {
            var productTypes = await dbContext.ProductTypes
                .Where(x => x.User != null && x.User.Id == userIdClaim)
                .ToListAsync();
            return productTypes;
        }
        public async Task<ProductType?> GetByIdAsync(Guid typeId, Guid userIdClaim)
        {
            var productType = await dbContext.ProductTypes
                .Where(x => x.Id == typeId && x.User != null && x.User.Id == userIdClaim)
                .FirstOrDefaultAsync();
            return productType;
        }
        public async Task AddAsync(ProductType productType)
        {
            await dbContext.ProductTypes.AddAsync(productType);
            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(ProductType productType, Guid userIdClaim)
        {
            var existingType = await dbContext.ProductTypes
                .FirstOrDefaultAsync(
                    x => x.Id == productType.Id && x.User != null && x.User.Id == userIdClaim);
            if (existingType != null)
            {
                dbContext.Entry(existingType).CurrentValues.SetValues(productType);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid productTypeId, Guid userIdClaim)
        {
            var typeToDelete = await dbContext.ProductTypes
                .Where(x => x.Id == productTypeId && x.User != null && x.User.Id == userIdClaim)
                .FirstOrDefaultAsync();
            if (typeToDelete != null)
            {
                var productsToUpdate = dbContext.Products
                    .Where(p => p.ProductType.Id == productTypeId)
                    .ToList();
                foreach (var product in productsToUpdate)
                {
                    product.Category = null;
                }
                dbContext.ProductTypes.Remove(typeToDelete);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
