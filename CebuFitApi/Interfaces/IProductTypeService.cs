using CebuFitApi.DTOs;

namespace CebuFitApi.Interfaces
{
    public interface IProductTypeService
    {
        Task<List<ProductTypeCreateDto>> GetAllCategoriesAsync(Guid userIdClaim);
        Task<ProductTypeDto> GetCategoryByIdAsync(Guid categoryId, Guid userIdClaim);
        Task CreateCategoryAsync(ProductTypeCreateDto productTypeDto, Guid userIdClaim);
        Task UpdateCategoryAsync(ProductTypeDto productTypeDto , Guid userIdClaim);
        Task DeleteCategoryAsync(Guid categoryId, Guid userIdClaim);
    }
}
