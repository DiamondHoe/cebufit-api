using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;

public interface IProductService
{
    Task<List<ProductDTO>> GetAllProductsAsync(Guid userIdClaim, DataType dataType);
    Task<List<ProductWithMacroDTO>> GetAllProductsWithMacroAsync(Guid userIdClaim, DataType dataType);
    Task<List<ProductWithCategoryDTO>> GetAllProductsWithCategoryAsync(Guid userIdClaim, DataType dataType);
    Task<List<ProductWithDetailsDTO>> GetAllProductsWithDetailsAsync(Guid userIdClaim, DataType dataType);
    Task<ProductDTO> GetProductByIdAsync(Guid productId, Guid userIdClaim);
    Task<ProductWithMacroDTO> GetProductByIdWithMacroAsync(Guid productId, Guid userIdClaim);
    Task<ProductWithCategoryDTO> GetProductByIdWithCategoryAsync(Guid productId, Guid userIdClaim);
    Task<ProductWithDetailsDTO> GetProductByIdWithDetailsAsync(Guid productId, Guid userIdClaim);
    Task CreateProductAsync(ProductCreateDTO product, Guid userIdClaim);
    Task UpdateProductAsync(ProductUpdateDTO product, Guid userIdClaim);
    Task DeleteProductAsync(Guid productId, Guid userIdClaim);
}
