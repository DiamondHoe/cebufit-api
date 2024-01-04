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
                .Include(c => c.Category)
                .ToListAsync();
            return products;
        }
        public async Task<List<Product>> GetAllWithMacroAsync()
        {
            var productsWithMacro = await _dbContext.Products
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .ToListAsync();

            return productsWithMacro;
        }
        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            var productsWithMacro = await _dbContext.Products
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .ToListAsync();

            return productsWithMacro;
        }
        public async Task<List<Product>> GetAllWithDetailsAsync()
        {
            var productsWithMacro = await _dbContext.Products
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .ToListAsync();

            return productsWithMacro;
        }
        public async Task<Product> GetByIdAsync(Guid productId)
        {
            var product = await _dbContext.Products
                .Include(c => c.Category)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<Product> GetByIdWithMacroAsync(Guid productId)
        {
            var product = await _dbContext.Products
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            return product;
        }
        public async Task<Product> GetByIdWithCategoryAsync(Guid productId)
        {
            var product = await _dbContext.Products
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            return product;
        }
        public async Task<Product> GetByIdWithDetailsAsync(Guid productId)
        {
            var product = await _dbContext.Products
                .Include(p => p.Macro)
                .Include(c => c.Category)
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
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                if (existingProduct?.Category?.Id != product?.Category?.Id || existingProduct?.Category?.Name != product?.Category?.Name)
                {
                    _dbContext.Products.Remove(existingProduct);
                    await _dbContext.SaveChangesAsync();

                    await _dbContext.Products.AddAsync(product);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
                    if (existingProduct.Macro != null)
                    {
                        product.Macro.Id = existingProduct.Macro.Id;
                        _dbContext.Entry(existingProduct.Macro).CurrentValues.SetValues(product.Macro);
                    }
                    await _dbContext.SaveChangesAsync();
                }
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
