using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(UserLoginDTO user);
        Task<bool> CreateAsync(UserCreateDTO user);
    }
}
