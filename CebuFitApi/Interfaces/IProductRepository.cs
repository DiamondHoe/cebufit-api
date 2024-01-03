using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetAllWithMacroAsync();
    Task<List<Product>> GetAllWithCategoryAsync();
    Task<List<Product>> GetAllWithDetailsAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task<Product> GetByIdWithMacroAsync(Guid id);
    Task<Product> GetByIdWithCategoryAsync(Guid id);
    Task<Product> GetByIdWithDetailsAsync(Guid id);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
}
