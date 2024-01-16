using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(User user);
        Task<bool> CreateAsync(User user);
    }
}
