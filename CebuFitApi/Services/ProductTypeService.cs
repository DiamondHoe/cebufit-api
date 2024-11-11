using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
namespace CebuFitApi.Services;

public class ProductTypeService(
    IProductTypeRepository productTypeRepository,
    IUserRepository userRepository,
    IMapper mapper) : IProductTypeService
{
    public async Task<List<ProductTypeDto>> GetAllProductTypesAsync(Guid userIdClaim, DataType dataType)
    {
        var productTypeEntities = await productTypeRepository.GetAllAsync(userIdClaim, dataType);
        var productTypeDtos = mapper.Map<List<ProductTypeDto>>(productTypeEntities);
        return productTypeDtos;
    }

    public async Task<ProductTypeDto?> GetProductTypeByIdAsync(Guid productTypeId, Guid userIdClaim)
    {
        var productTypeEntity = await productTypeRepository.GetByIdAsync(productTypeId, userIdClaim);
        var productTypeDto = mapper.Map<ProductTypeDto>(productTypeEntity);
        return productTypeDto;
    }
    public async Task CreateProductTypeAsync(ProductTypeCreateDto productTypeDto, Guid userIdClaim)
    {
        var productType = mapper.Map<ProductType>(productTypeDto);
        productType.Id = Guid.NewGuid();

        var foundUser = await userRepository.GetById(userIdClaim);
        if(foundUser != null)
        {
            productType.User = foundUser;
            await productTypeRepository.AddAsync(productType);
        }

    }

    public async Task UpdateProductTypeAsync(ProductTypeDto productTypeDto, Guid userIdClaim)
    {
        var productType = mapper.Map<ProductType>(productTypeDto);
        var foundUser = await userRepository.GetById(userIdClaim);
        if (foundUser != null) await productTypeRepository.UpdateAsync(productType, userIdClaim);
    }

    public async Task DeleteProductTypeAsync(Guid productTypeId, Guid userIdClaim)
    {
        await productTypeRepository.DeleteAsync(productTypeId, userIdClaim);
    }
}
