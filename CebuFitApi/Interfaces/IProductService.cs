using CebuFitApi.DTOs;
using CebuFitApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductService
{
    Task<ProductDTO> CreateProductAsync(ProductDTO product);
    Task<ProductDTO> GetProductByIdAsync(Guid productId);
    Task<List<ProductDTO>> GetAllProductsAsync();
    Task UpdateProductAsync(ProductDTO product);
    Task DeleteProductAsync(Guid productId);
}
