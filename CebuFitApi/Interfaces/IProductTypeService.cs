using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.Interfaces
{
    public interface IProductTypeService
    {
        Task<List<ProductTypeDto>> GetAllProductTypesAsync(Guid userIdClaim, DataType dataType);
        Task<ProductTypeDto?> GetProductTypeByIdAsync(Guid productTypeId, Guid userIdClaim);
        Task CreateProductTypeAsync(ProductTypeCreateDto productTypeDto, Guid userIdClaim);
        Task UpdateProductTypeAsync(ProductTypeDto productTypeDto , Guid userIdClaim);
        Task DeleteProductTypeAsync(Guid productTypeId, Guid userIdClaim);
    }
}
