using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductService
{
    Task<List<ProductDTO>> GetAllProductsAsync();
    Task<List<ProductWithMacroDTO>> GetAllProductsWithMacroAsync();
    Task<ProductDTO> GetProductByIdAsync(Guid productId);
    Task<ProductWithMacroDTO> GetProductByIdWithMacroAsync(Guid productId);
    Task CreateProductAsync(ProductCreateDTO product);
    Task UpdateProductAsync(ProductUpdateDTO product);
    Task DeleteProductAsync(Guid productId);
}
