using CebuFitApi.DTOs;
using CebuFitApi.DTOs.User;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(UserLoginDTO userDTO);
        Task<(bool, User)> CreateAsync(UserCreateDTO userDTO);
        Task<UserDTO> GetByEmailAsync(string email);
        Task<UserDetailsDTO> GetDetailsAsync(Guid userIdClaim);
        Task<string> ResetPasswordAsync(string email);
        Task UpdateAsync(Guid userIdClaim, UserUpdateDTO userDTO);
        Task<bool> DeleteAsync(Guid userIdClaim);
        Task<SummaryDTO> GetSummaryAsync(Guid userIdClaim, DateTime start, DateTime end);
    }
}
