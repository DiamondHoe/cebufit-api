using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(Guid userIdClaim, DataType dataType);
    //Task<List<Product>> GetAllWithMacroAsync(Guid userIdClaim, DataType dataType);
    //Task<List<Product>> GetAllWithCategoryAsync(Guid userIdClaim, DataType dataType);
    //Task<List<Product>> GetAllWithDetailsAsync(Guid userIdClaim, DataType dataType);
    Task<Product> GetByIdAsync(Guid id, Guid userIdClaim);
    //Task<Product> GetByIdWithMacroAsync(Guid id, Guid userIdClaim);
    //Task<Product> GetByIdWithCategoryAsync(Guid id, Guid userIdClaim);
    Task<Product> GetByIdWithDetailsAsync(Guid id, Guid userIdClaim);
    Task CreateAsync(Product product, Guid userIdClaim);
    Task UpdateAsync(Product product, Guid userIdClaim);
    Task DeleteAsync(Guid id, Guid userIdClaim);
}
