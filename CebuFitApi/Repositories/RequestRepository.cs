using CebuFitApi.Data;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly CebuFitApiDbContext _dbContext;
    
    public RequestRepository(CebuFitApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Request>> GetAllAsync()
    {
        return await _dbContext.Requests.ToListAsync();
    }
    
    public async Task<List<Request>> GetByStatus(RequestStatus requestStatus)
    {
        return await _dbContext.Requests
            .Where(x => x.Status == requestStatus)
            .ToListAsync();
    }
    
    public async Task<List<Request>> GetByType(RequestType requestType)
    {
        return await _dbContext.Requests
            .Where(x => x.Type == requestType)
            .ToListAsync();
    }
    
    public async Task<List<Request>> GetByTypeAndStatus(RequestType requestType, RequestStatus requestStatus)
    {
        return await _dbContext.Requests
            .Where(x => x.Type == requestType && x.Status == requestStatus)
            .Include(r => r.Requester)
            .Include(r => r.Approver)
            .ToListAsync();
    }
    
    public async Task<Request?> GetByIdAsync(Guid id)
    {
        var request = await _dbContext.Requests
            .Include(r => r.Requester)
            .Include(r => r.Approver)
            .FirstOrDefaultAsync(x => x.Id == id);
        return request;
    }
    
    public async Task CreateAsync(Request request)
    {
        await _dbContext.Requests.AddAsync(request);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Request request)
    {
        _dbContext.Requests.Update(request);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var request = await GetByIdAsync(id);
        if (request != null)
        {
            _dbContext.Requests.Remove(request);
            await _dbContext.SaveChangesAsync();
        }
    }
}