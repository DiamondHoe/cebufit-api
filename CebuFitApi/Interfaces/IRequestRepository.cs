using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces;

public interface IRequestRepository
{
    Task<List<Request>> GetAllAsync();
    Task<Request?> GetByIdAsync(Guid id);
    Task<List<Request>> GetByType(RequestType requestType);
    Task<List<Request>> GetByStatus(RequestStatus requestStatus);
    Task<List<Request>> GetByTypeAndStatus(RequestType requestType, RequestStatus requestStatus);
    
    Task<bool> CreateAsync(Request request);
    Task UpdateAsync(Request request);
    Task DeleteAsync(Guid id);
}