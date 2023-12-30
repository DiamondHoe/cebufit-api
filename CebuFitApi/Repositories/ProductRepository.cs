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
        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _dbContext.Products
                .Include(p => p.Macro)
                .ToListAsync();
            return products;
        }
        public async Task<List<Product>> GetAllWithMacroAsync()
        {
            var productsWithMacro = await _dbContext.Products
                .Include(p => p.Macro)
                .ToListAsync();

            return productsWithMacro;
        }
        public async Task<Product> GetByIdAsync(Guid productId)
        {
            var product = await _dbContext.Products
                .Include(p => p.Macro)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<Product> GetByIdWithMacroAsync(Guid productId)
        {
            var product = await _dbContext.Products
                .Include(p => p.Macro)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            return product;
        }
        public async Task CreateAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _dbContext.Products
                .Include(p => p.Macro)
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
                if(existingProduct.Macro != null)
                {
                    product.Macro.Id = existingProduct.Macro.Id;
                    _dbContext.Entry(existingProduct.Macro).CurrentValues.SetValues(product.Macro);
                }

                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(Guid id)
        {
            var productToDelete = await _dbContext.Products.FindAsync(id);
            if (productToDelete != null)
            {
                _dbContext.Products.Remove(productToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
