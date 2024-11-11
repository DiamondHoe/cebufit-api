using CebuFitApi.Data;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class StorageItemRepository : IStorageItemRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public StorageItemRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<StorageItem>> GetAllAsync(Guid userIdClaim)
        {
            var storageItems = await _dbContext.StorageItems
                .Where(x => x.User.Id == userIdClaim)
                .ToListAsync();
            return storageItems;
        }
        public async Task<List<StorageItem>> GetAllWithProductAsync(Guid userIdClaim, bool withoutEaten = false)
        {
            var storageItems = await _dbContext.StorageItems
                .Where(x => x.User.Id == userIdClaim)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ProductType)
                .Include(x => x.Product)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Product)
                    .ThenInclude(x => x.Macro)
                .ToListAsync();
            if (withoutEaten)
            {
                storageItems = storageItems.Where(item => item.ActualWeight > 0 && item.ActualQuantity > 0).ToList();
            }
            return storageItems;
        }
        public async Task<List<StorageItem>> GetAllByProductIdWithProductAsync(Guid productId, Guid userIdClaim)
        {
            var storageItems = await _dbContext.StorageItems
                .Where(x => x.User.Id == userIdClaim && x.Product.Id == productId)
                .Include(x => x.Product)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Product)
                    .ThenInclude(x => x.Macro)
                .ToListAsync();
            return storageItems;
        }
        public async Task<StorageItem> GetByIdAsync(Guid id, Guid userIdClaim)
        {
            var storageItem = await _dbContext.StorageItems
                .Where(si => si.Id == id && si.User.Id == userIdClaim)
                .FirstOrDefaultAsync();
            return storageItem;
        }
        public async Task<StorageItem> GetByIdWithProductAsync(Guid id, Guid userIdClaim)
        {
            var storageItem = await _dbContext.StorageItems
                .Where(si => si.Id == id && si.User.Id == userIdClaim)
                .Include(x => x.Product)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Product)
                    .ThenInclude(x => x.Macro)
                .FirstOrDefaultAsync();
            return storageItem;
        }
        public async Task CreateAsync(StorageItem storageItem, Guid userIdClaim)
        {
            await _dbContext.StorageItems.AddAsync(storageItem);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(StorageItem storageItem, Guid userIdClaim)
        {
            var existingStorageItem = await _dbContext.StorageItems
                .FirstOrDefaultAsync(si => si.Id == storageItem.Id && si.User.Id == userIdClaim);

            if (existingStorageItem != null) 
            {
                _dbContext.Entry(existingStorageItem).CurrentValues.SetValues(storageItem);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id, Guid userIdClaim)
        {
            var storageItemToDelete = await _dbContext.StorageItems
                 .Where(x => x.Id == id && x.User.Id == userIdClaim)
                 .FirstAsync();
            if (storageItemToDelete != null) 
            {
                _dbContext.StorageItems.Remove(storageItemToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
