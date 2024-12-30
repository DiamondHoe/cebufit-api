using System.Net;
using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Services;

public class RequestService(
    IRequestRepository requestRepository,
    IProductRepository productRepository,
    IRecipeRepository recipeRepository,
    IProductTypeRepository productTypeRepository,
    ICategoryRepository categoryRepository,
    IUserRepository userRepository,
    IMapper mapper) : IRequestService
{
    public async Task<List<RequestDto>> GetAllRequestsAsync()
    {
        var requestsEntities = await requestRepository.GetAllAsync();
        var requestDtoList = mapper.Map<List<RequestDto>>(requestsEntities);
        return requestDtoList;
    }
    
    public async Task<List<RequestDto>> GetRequestsByStatus(RequestStatus requestStatus)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<RequestDto>> GetRequestsByType(RequestType requestType)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<RequestDto>> GetRequestsByTypeAndStatus(RequestType requestType, RequestStatus requestStatus)
    {
        var requestsEntities = await requestRepository.GetByTypeAndStatus(requestType, requestStatus);
        var requestDtoList = mapper.Map<List<RequestDto>>(requestsEntities);
        return requestDtoList;
    }
    
    public async Task<List<RequestProductWithDetailsDto>> GetRequestsProductByStatusWithDetails(RequestStatus requestStatus)
    {
        var requestsEntities = await requestRepository.GetByTypeAndStatus(RequestType.PromoteProductToPublic, requestStatus);
        var requestDtoList = mapper.Map<List<RequestProductWithDetailsDto>>(requestsEntities);
        foreach (var request in requestsEntities)
        {
            var dto = requestDtoList.Find(r => r.Id == request.Id);
            if (dto == null) throw new Exception("Request not found");
            var product = await productRepository.GetByIdWithDetailsAsync(request.RequestedItemId, request.Requester.Id);
            var productDto = mapper.Map<ProductWithDetailsDTO>(product);
            dto.RequestedProduct = productDto;
        }
        return requestDtoList;
    }
    
    public async Task<List<RequestRecipeWithDetailsDto>> GetRequestsRecipeByStatusWithDetails(
        RequestStatus requestStatus)
    {
        var requestsEntities = await requestRepository.GetByTypeAndStatus(RequestType.PromoteRecipeToPublic, requestStatus);
        var requestDtoList = mapper.Map<List<RequestRecipeWithDetailsDto>>(requestsEntities);
        foreach (var request in requestsEntities)
        {
            var dto = requestDtoList.Find(r => r.Id == request.Id);
            if (dto == null) throw new Exception("Request not found");
            var recipe = await recipeRepository.GetByIdWithDetailsAsync(request.RequestedItemId, request.Requester.Id);
            var recipeDto = mapper.Map<RecipeWithDetailsDTO>(recipe);
            dto.RequestedRecipe = recipeDto;
        }
        return requestDtoList;
    }
    
    public async Task<List<RequestProductTypeDto>> GetRequestsProductTypeByStatus(
        RequestStatus requestStatus)
    {
        var requestsEntities = await requestRepository.GetByTypeAndStatus(RequestType.PromoteProductTypeToPublic, requestStatus);
        var requestDtoList = mapper.Map<List<RequestProductTypeDto>>(requestsEntities);
        foreach (var request in requestsEntities)
        {
            var dto = requestDtoList.Find(r => r.Id == request.Id);
            if (dto == null) throw new Exception("Request not found");
            var productType = await productTypeRepository.GetByIdAsync(request.RequestedItemId, request.Requester.Id);
            var productTypeDto = mapper.Map<ProductTypeDto>(productType);
            dto.RequestedProductType = productTypeDto;
        }
        return requestDtoList;
    }
    
    public async Task<List<RequestCategoryDto>> GetRequestsCategoriesByStatus(
        RequestStatus requestStatus)
    {
        var requestsEntities = await requestRepository.GetByTypeAndStatus(RequestType.PromoteCategoryToPublic, requestStatus);
        var requestDtoList = mapper.Map<List<RequestCategoryDto>>(requestsEntities);
        foreach (var request in requestsEntities)
        {
            var dto = requestDtoList.Find(r => r.Id == request.Id);
            if (dto == null) throw new Exception("Request not found");
            var category = await categoryRepository.GetByIdAsync(request.RequestedItemId, request.Requester.Id);
            var categoryDto = mapper.Map<CategoryDTO>(category);
            dto.RequestedCategory = categoryDto;
        }
        return requestDtoList;
    }
    
    public async Task<RequestDto> GetRequestByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> CreateRequestAsync(RequestCreateDto requestCreateDto, Guid userIdClaim)
    {
        var requestEntity = mapper.Map<Request>(requestCreateDto);
        requestEntity.Requester = userRepository.GetByIdAsync(userIdClaim).Result;
        requestEntity.Status = RequestStatus.Pending;
        
        bool requestAlreadyCreated = await requestRepository.CreateAsync(requestEntity);
        return requestAlreadyCreated;
    }
    
    public async Task UpdateRequestAsync(RequestDto request, Guid userIdClaim)
    {
        throw new NotImplementedException();
    }
    
    public async Task DeleteRequestAsync(Guid id, Guid userIdClaim)
    {
        throw new NotImplementedException();
    }
    
    public async Task ChangeRequestStatusAsync(Guid id, RequestStatus requestStatus, Guid userIdClaim)
    {
        var requestEntity = await requestRepository.GetByIdAsync(id);
        if (requestEntity == null) return;
        var requestType = requestEntity.Type;
        requestEntity.Status = requestStatus;
        requestEntity.Approver = await userRepository.GetByIdAsync(userIdClaim);
        
        switch (requestType)
        {
            case RequestType.PromoteProductToPublic:
                var product = await productRepository.GetByIdAsync(requestEntity.RequestedItemId, requestEntity.Requester.Id);
                if (product == null) return;
        
                if (requestStatus == RequestStatus.Approved)
                {
                    product.IsPublic = true;
                    await productRepository.UpdateAsync(product, requestEntity.Requester.Id);
                }
                break;
            case RequestType.PromoteRecipeToPublic:
                var recipe = await recipeRepository.GetByIdAsync(requestEntity.RequestedItemId, requestEntity.Requester.Id);
                if (recipe == null) return;
        
                if (requestStatus == RequestStatus.Approved)
                {
                    recipe.IsPublic = true;
                    await recipeRepository.UpdateAsync(recipe, requestEntity.Requester.Id);
                }
                break;
            case RequestType.PromoteProductTypeToPublic:
                var productType = await productTypeRepository.GetByIdAsync(requestEntity.RequestedItemId, requestEntity.Requester.Id);
                if (productType == null) return;
        
                if (requestStatus == RequestStatus.Approved)
                {
                    productType.IsPublic = true;
                    await productTypeRepository.UpdateAsync(productType, requestEntity.Requester.Id);
                }
                break;
            case RequestType.PromoteCategoryToPublic:
                var category = await categoryRepository.GetByIdAsync(requestEntity.RequestedItemId, requestEntity.Requester.Id);
                if (category == null) return;
        
                if (requestStatus == RequestStatus.Approved)
                {
                    category.IsPublic = true;
                    await categoryRepository.UpdateAsync(category, requestEntity.Requester.Id);
                }
                break;
        }
        await requestRepository.UpdateAsync(requestEntity);
    }
}