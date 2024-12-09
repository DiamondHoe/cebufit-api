using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IProductTypeRepository
    {
        Task<List<ProductType>> GetAllAsync(Guid userIdClaim, DataType dataType);
        Task<ProductType?> GetByIdAsync(Guid productTypeId, Guid userIdClaim);
        Task AddAsync(ProductType productType);
        Task UpdateAsync(ProductType productType, Guid userIdClaim);
        Task DeleteAsync(Guid categoryId, Guid userIdClaim);
    }
}
