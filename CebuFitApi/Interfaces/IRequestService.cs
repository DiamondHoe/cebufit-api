using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.Interfaces;

public interface IRequestService
{
    Task<List<RequestDto>> GetAllRequestsAsync();
    Task<RequestDto> GetRequestByIdAsync(Guid id);
    Task<List<RequestDto>> GetRequestsByType(RequestType requestType);
    Task<List<RequestDto>> GetRequestsByStatus(RequestStatus requestStatus);
    Task<List<RequestDto>> GetRequestsByTypeAndStatus(RequestType requestType, RequestStatus requestStatus);
    Task<List<RequestProductWithDetailsDto>> GetRequestsProductByStatusWithDetails(RequestStatus requestStatus);
    Task<List<RequestRecipeWithDetailsDto>> GetRequestsRecipeByStatusWithDetails(RequestStatus requestStatus);
    
    Task<bool> CreateRequestAsync(RequestCreateDto requestCreate, Guid userIdClaim);
    Task UpdateRequestAsync(RequestDto request, Guid userIdClaim);
    Task DeleteRequestAsync(Guid id, Guid userIdClaim);
    Task ChangeRequestStatusAsync(Guid id, RequestStatus requestStatus, Guid userIdClaim);
}