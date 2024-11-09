using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid userId);
        Task<User> AuthenticateAsync(User user);
        Task<bool> CreateAsync(User user);
        Task<string> ResetPasswordAsync(string email);
        Task<string> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid userId);

    }
}
