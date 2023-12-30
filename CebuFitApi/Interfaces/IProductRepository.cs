using CebuFitApi.DTOs;
using CebuFitApi.Models;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetAllWithMacroAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task<Product> GetByIdWithMacroAsync(Guid id);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
}
