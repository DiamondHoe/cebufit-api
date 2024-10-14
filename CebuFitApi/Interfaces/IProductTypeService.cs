using CebuFitApi.DTOs;

namespace CebuFitApi.Interfaces
{
    public interface IProductTypeService
    {
        Task<List<ProductTypeDto>> GetAllProductTypesAsync(Guid userIdClaim);
        Task<ProductTypeDto?> GetProductTypeByIdAsync(Guid productTypeId, Guid userIdClaim);
        Task CreateProductTypeAsync(ProductTypeCreateDto productTypeDto, Guid userIdClaim);
        Task UpdateProductTypeAsync(ProductTypeDto productTypeDto , Guid userIdClaim);
        Task DeleteProductTypeAsync(Guid productTypeId, Guid userIdClaim);
    }
}
