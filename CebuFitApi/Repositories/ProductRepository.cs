using CebuFitApi.Data;
using CebuFitApi.DTOs;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public ProductRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Product>> GetAllAsync(Guid userIdClaim)
        {
            var products = await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim)
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .ToListAsync();
            return products;
        }
        public async Task<List<Product>> GetAllWithMacroAsync(Guid userIdClaim)
        {
            var productsWithMacro = await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim)
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .ToListAsync();

            return productsWithMacro;
        }
        public async Task<List<Product>> GetAllWithCategoryAsync(Guid userIdClaim)
        {
            var productsWithMacro = await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim)
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .ToListAsync();

            return productsWithMacro;
        }
        public async Task<List<Product>> GetAllWithDetailsAsync(Guid userIdClaim)
        {
            var productsWithMacro = await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim)
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .ToListAsync();

            return productsWithMacro;
        }
        public async Task<Product> GetByIdAsync(Guid productId, Guid userIdClaim)
        {
            var product = await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim)
                .Include(c => c.Category)
                .Where(p => p.Id == productId)
                .FirstAsync();

            return product;
        }

        public async Task<Product> GetByIdWithMacroAsync(Guid productId, Guid userIdClaim)
        {
            var product = await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim)
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .Where(p => p.Id == productId)
                .FirstAsync();

            return product;
        }
        public async Task<Product> GetByIdWithCategoryAsync(Guid productId, Guid userIdClaim)
        {
            var product = await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim)
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .Where(p => p.Id == productId)
                .FirstAsync();

            return product;
        }
        public async Task<Product> GetByIdWithDetailsAsync(Guid productId, Guid userIdClaim)
        {
            var product = await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim)
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .Where(p => p.Id == productId)
                .FirstAsync();

            return product;
        }
        public async Task CreateAsync(Product product, Guid userIdClaim)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product, Guid userIdClaim)
        {
            var existingProduct = await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim)
                .Include(p => p.Macro)
                .Include(p => p.Category)
                .FirstAsync(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                _dbContext.Entry(existingProduct).State = EntityState.Detached;
                _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
                existingProduct.Category = product.Category;
                _dbContext.Attach(existingProduct);
                _dbContext.Entry(existingProduct).State = EntityState.Modified;

                product.Macro.Id = existingProduct.Macro.Id;
                _dbContext.Entry(existingProduct.Macro).CurrentValues.SetValues(product.Macro);


                await _dbContext.SaveChangesAsync();
            }
        }



        public async Task DeleteAsync(Guid id, Guid userIdClaim)
        {
            var productToDelete = await _dbContext.Products
                .Where(p => p.Id == id && p.User.Id == userIdClaim)
                .FirstAsync();
            if (productToDelete != null)
            {
                _dbContext.Products.Remove(productToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
