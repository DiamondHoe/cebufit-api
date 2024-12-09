using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByEmailAsync(string email);
        Task<User> AuthenticateAsync(User user);
        Task<bool> CreateAsync(User user);

        Task UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid userId);

    }
}
