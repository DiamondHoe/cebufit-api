﻿using CebuFitApi.Data;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
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
        public async Task<List<Product>> GetAllAsync(Guid userIdClaim, DataType dataType)
        {
            return dataType switch
            {
                DataType.Private => await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim && x.IsPublic == false)
                .Include(p => p.Macro)
                .Include(pt => pt.ProductType)
                .Include(c => c.Category)
                .ToListAsync(),

                DataType.Public => await _dbContext.Products
                .Where(x => x.IsPublic == true)
                .Include(p => p.Macro)
                .Include(pt => pt.ProductType)
                .Include(c => c.Category)
                .ToListAsync(),

                DataType.Both => await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim || x.IsPublic == true)
                .Include(p => p.Macro)
                .Include(pt => pt.ProductType)
                .Include(c => c.Category)
                .ToListAsync(),

                _ => await _dbContext.Products
                .Where(x => x.User.Id == userIdClaim)
                .Include(p => p.Macro)
                .Include(pt => pt.ProductType)
                .Include(c => c.Category)
                .ToListAsync(),
            };
        }
        //Nie używane, złamany DRY

        //public async Task<List<Product>> GetAllWithMacroAsync(Guid userIdClaim, DataType dataType)
        //{
        //    var productsWithMacro = await _dbContext.Products
        //        .Where(x => x.User.Id == userIdClaim)
        //        .Include(p => p.Macro)
        //        .Include(c => c.Category)
        //        .ToListAsync();

        //    return productsWithMacro;
        //}
        //public async Task<List<Product>> GetAllWithCategoryAsync(Guid userIdClaim, DataType dataType)
        //{
        //    var productsWithMacro = await _dbContext.Products
        //        .Where(x => x.User.Id == userIdClaim)
        //        .Include(p => p.Macro)
        //        .Include(c => c.Category)
        //        .ToListAsync();

        //    return productsWithMacro;
        //}
        //public async Task<List<Product>> GetAllWithDetailsAsync(Guid userIdClaim, DataType dataType)
        //{
        //    var productsWithMacro = await _dbContext.Products
        //        .Where(x => x.User.Id == userIdClaim)
        //        .Include(p => p.Macro)
        //        .Include(c => c.Category)
        //        .ToListAsync();

        //    return productsWithMacro;
        //}
        public async Task<Product?> GetByIdAsync(Guid productId, Guid userIdClaim)
        {
            var product = await _dbContext.Products
                .Include(c => c.Category)
                .Include(t => t.ProductType)
                .Include(m => m.Macro)
                .FirstOrDefaultAsync(x => x.Id == productId);
 
            return product;
        }

        //Nie używane, złamany DRY
        //public async Task<Product> GetByIdWithMacroAsync(Guid productId, Guid userIdClaim)
        //{
        //    var product = await _dbContext.Products
        //        .Where(x => x.User.Id == userIdClaim && x.Id == productId)
        //        .Include(p => p.Macro)
        //        .Include(c => c.Category)
        //        .FirstAsync();

        //    return product;
        //}
        //public async Task<Product> GetByIdWithCategoryAsync(Guid productId, Guid userIdClaim)
        //{
        //    var product = await _dbContext.Products
        //        .Where(x => x.User.Id == userIdClaim && x.Id == productId)
        //        .Include(p => p.Macro)
        //        .Include(c => c.Category)
        //        .FirstAsync();

        //    return product;
        //}
        public async Task<Product> GetByIdWithDetailsAsync(Guid productId, Guid userIdClaim)
        {
            var product = await _dbContext.Products
                .Include(p => p.Macro)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(x => x.User.Id == userIdClaim && x.Id == productId);

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
                .Include(p => p.Macro)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(x => x.User.Id == userIdClaim && x.Id == product.Id);

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

        public async Task DeleteAsync(Guid id)
        {
            var productToDelete = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id);
            if (productToDelete != null)
            {
                _dbContext.Products.Remove(productToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
