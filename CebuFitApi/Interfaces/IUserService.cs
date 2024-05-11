using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(UserLoginDTO user);
        Task<bool> CreateAsync(UserCreateDTO user);
        Task<string> ResetPasswordAsync(string email);
        Task<string> UpdateAsync(UserDTO user);
        Task<bool> DeleteAsync(Guid userId);
        Task<SummaryDTO> GetSummaryAsync(Guid userId, DateTime start, DateTime end);
    }
}
