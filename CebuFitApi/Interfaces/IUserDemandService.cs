using CebuFitApi.DTOs.Demand;

namespace CebuFitApi.Interfaces
{
    public interface IUserDemandService
    {
        Task<UserDemandDTO> GetDemandAsync(Guid userId);
        Task UpdateDemandAsync(UserDemandUpdateDTO demandUpdateDTO, Guid userId);
        Task AutoCalculateDemandAsync(Guid userId);
    }
}
