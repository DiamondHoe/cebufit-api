using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IDemandRepository
    {
        Task<Demand> GetDemandAsync(Guid userId);
        Task UpdateDemandAsync(Demand demand, Guid userId);
    }
}
