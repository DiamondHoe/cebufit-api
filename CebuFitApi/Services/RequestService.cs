using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;

namespace CebuFitApi.Services;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMapper _mapper;
    
    public RequestService(
        IRequestRepository requestRepository,
        IMapper mapper)
    {
        _requestRepository = requestRepository;
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
        throw new NotImplementedException();
    }
    
    public async Task<RequestDto> GetRequestByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task CreateRequestAsync(RequestCreateDto request, Guid userIdClaim)
    {
        throw new NotImplementedException();
    }
    
    public async Task UpdateRequestAsync(RequestDto request, Guid userIdClaim)
    {
        throw new NotImplementedException();
    }
    
    public async Task DeleteRequestAsync(Guid id, Guid userIdClaim)
    {
        throw new NotImplementedException();
    }
    
    public async Task ApproveRequestAsync(Guid id, RequestStatus requestStatus)
    {
        throw new NotImplementedException();
    }
    
    public async Task RejectRequestAsync(Guid id, RequestStatus requestStatus)
    {
        throw new NotImplementedException();
    }
}