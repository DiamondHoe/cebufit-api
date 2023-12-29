using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class ProductService : IProductService
    {
        public Task<ProductDTO> CreateProductAsync(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetProductByIdAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(ProductDTO product)
        {
            throw new NotImplementedException();
        }
    }
}
