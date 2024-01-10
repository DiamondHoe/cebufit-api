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
        public async Task<List<StorageItem>> GetAllAsync()
        {
            var storageItems = await _dbContext.StorageItems
                .ToListAsync();
            return storageItems;
        }
        public async Task<List<StorageItem>> GetAllWithProductAsync()
        {
            var storageItems = await _dbContext.StorageItems
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .Include(x => x.Product)
                .ThenInclude(x => x.Macro)
                .ToListAsync();
            return storageItems;
        }
        public async Task<StorageItem> GetByIdAsync(Guid id)
        {
            var storageItem = await _dbContext.StorageItems
                .Where(si => si.Id == id)
                .FirstOrDefaultAsync();
            return storageItem;
        }
        public async Task<StorageItem> GetByIdWithProductAsync(Guid id)
        {
            var storageItem = await _dbContext.StorageItems
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .Include(x => x.Product)
                .ThenInclude(x => x.Macro)
                .Where(si => si.Id == id)
                .FirstOrDefaultAsync();
            return storageItem;
        }
        public async Task CreateAsync(StorageItem storageItem)
        {
            await _dbContext.StorageItems.AddAsync(storageItem);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(StorageItem storageItem)
        {
            var existingStorageItem = await _dbContext.StorageItems
                .FirstOrDefaultAsync(si => si.Id == storageItem.Id);

            if (existingStorageItem != null) 
            {
                _dbContext.Entry(existingStorageItem).CurrentValues.SetValues(storageItem);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id)
        {
           var storageItemToDelete = await _dbContext.StorageItems.FindAsync(id);
            if (storageItemToDelete != null) 
            {
                _dbContext.StorageItems.Remove(storageItemToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
