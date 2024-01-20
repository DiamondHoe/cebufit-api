using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductService
{
    Task<List<ProductDTO>> GetAllProductsAsync(Guid userIdClaim);
    Task<List<ProductWithMacroDTO>> GetAllProductsWithMacroAsync(Guid userIdClaim);
    Task<List<ProductWithCategoryDTO>> GetAllProductsWithCategoryAsync(Guid userIdClaim);
    Task<List<ProductWithDetailsDTO>> GetAllProductsWithDetailsAsync(Guid userIdClaim);
    Task<ProductDTO> GetProductByIdAsync(Guid productId, Guid userIdClaim);
    Task<ProductWithMacroDTO> GetProductByIdWithMacroAsync(Guid productId, Guid userIdClaim);
    Task<ProductWithCategoryDTO> GetProductByIdWithCategoryAsync(Guid productId, Guid userIdClaim);
    Task<ProductWithDetailsDTO> GetProductByIdWithDetailsAsync(Guid productId, Guid userIdClaim);
    Task CreateProductAsync(ProductCreateDTO product, Guid userIdClaim);
    Task UpdateProductAsync(ProductUpdateDTO product, Guid userIdClaim);
    Task DeleteProductAsync(Guid productId, Guid userIdClaim);
}
