using System.Net;
using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Services;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IProductRepository _productRepository;
    public readonly IRecipeRepository _recipeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public RequestService(
        IRequestRepository requestRepository,
        IProductRepository productRepository,
        IRecipeRepository recipeRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _requestRepository = requestRepository;
        _productRepository = productRepository;
        _recipeRepository = recipeRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<List<RequestDto>> GetAllRequestsAsync()
    {
        var requestsEntities = await _requestRepository.GetAllAsync();
        var requestDtoList = _mapper.Map<List<RequestDto>>(requestsEntities);
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
        var requestsEntities = await _requestRepository.GetByTypeAndStatus(requestType, requestStatus);
        var requestDtoList = _mapper.Map<List<RequestDto>>(requestsEntities);
        return requestDtoList;
    }
    
    public async Task<List<RequestProductWithDetailsDto>> GetRequestsProductByStatusWithDetails(RequestStatus requestStatus)
    {
        var requestsEntities = await _requestRepository.GetByTypeAndStatus(RequestType.PromoteProductToPublic, requestStatus);
        var requestDtoList = _mapper.Map<List<RequestProductWithDetailsDto>>(requestsEntities);
        foreach (var request in requestsEntities)
        {
            var dto = requestDtoList.Find(r => r.Id == request.Id);
            if (dto == null) throw new Exception("Request not found");
            var product = await _productRepository.GetByIdWithDetailsAsync(request.RequestedItemId, request.Requester.Id);
            var productDto = _mapper.Map<ProductWithDetailsDTO>(product);
            dto.RequestedProduct = productDto;
        }
        return requestDtoList;
    }
    
    public async Task<List<RequestRecipeWithDetailsDto>> GetRequestsRecipeByStatusWithDetails(RequestStatus requestStatus)
    {
        var requestsEntities = await _requestRepository.GetByTypeAndStatus(RequestType.PromoteRecipeToPublic, requestStatus);
        var requestDtoList = _mapper.Map<List<RequestRecipeWithDetailsDto>>(requestsEntities);
        foreach (var request in requestsEntities)
        {
            var dto = requestDtoList.Find(r => r.Id == request.Id);
            if (dto == null) throw new Exception("Request not found");
            var recipe = await _recipeRepository.GetByIdWithDetailsAsync(request.RequestedItemId, request.Requester.Id);
            var recipeDto = _mapper.Map<RecipeWithDetailsDTO>(recipe);
            dto.RequestedRecipe = recipeDto;
        }
        return requestDtoList;
    }
    
    public async Task<RequestDto> GetRequestByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> CreateRequestAsync(RequestCreateDto requestCreateDto, Guid userIdClaim)
    {
        var requestEntity = _mapper.Map<Request>(requestCreateDto);
        requestEntity.Requester = _userRepository.GetById(userIdClaim).Result;
        requestEntity.Status = RequestStatus.Pending;
        
        bool requestAlreadyCreated = await _requestRepository.CreateAsync(requestEntity);
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
        var requestEntity = await _requestRepository.GetByIdAsync(id);
        if (requestEntity == null) return;
        var requestType = requestEntity.Type;
        requestEntity.Status = requestStatus;
        requestEntity.Approver = await _userRepository.GetById(userIdClaim);
        
        switch (requestType)
        {
            case RequestType.PromoteProductToPublic:
                var product = await _productRepository.GetByIdAsync(requestEntity.RequestedItemId, requestEntity.Requester.Id);
                if (product == null) return;
        
                if (requestStatus == RequestStatus.Approved)
                {
                    product.IsPublic = true;
                    await _productRepository.UpdateAsync(product, requestEntity.Requester.Id);
                }
                break;
            case RequestType.PromoteRecipeToPublic:
                var recipe = await _recipeRepository.GetByIdAsync(requestEntity.RequestedItemId, requestEntity.Requester.Id);
                if (recipe == null) return;
        
                if (requestStatus == RequestStatus.Approved)
                {
                    recipe.IsPublic = true;
                    await _recipeRepository.UpdateAsync(recipe, requestEntity.Requester.Id);
                }
                break;
        }
        await _requestRepository.UpdateAsync(requestEntity);
    }
}