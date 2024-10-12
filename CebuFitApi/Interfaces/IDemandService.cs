using CebuFitApi.DTOs.Demand;

namespace CebuFitApi.Interfaces
{
    public interface IDemandService
    {
        Task<DemandDTO> GetDemandAsync(Guid userId);
        Task UpdateDemandAsync(DemandUpdateDTO demandUpdateDTO, Guid userId);
        Task AutoCalculateDemandAsync(Guid userId);
    }
}
