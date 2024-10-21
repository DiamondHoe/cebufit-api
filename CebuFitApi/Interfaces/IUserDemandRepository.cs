using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IUserDemandRepository
    {
        Task<UserDemand?> GetDemandAsync(Guid userId);
        Task UpdateDemandAsync(UserDemand demand, Guid userId);
    }
}
