using CebuFitApi.Data;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public UserRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetById(Guid userId)
        {
            var foundUser =  await _dbContext.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
            return foundUser;
        }
        public async Task<User> AuthenticateAsync(User user)
        {   
            var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == user.Login);

            if (foundUser != null && BCrypt.Net.BCrypt.Verify(user.Password, foundUser.Password))
            {
                return foundUser;
            }

            return null;
        }

        public async Task<bool> CreateAsync(User user)
        {
            try
            {
                // Check if the username already exists
                bool usernameExists = await _dbContext.Users.AnyAsync(u => u.Login == user.Login);

                if (usernameExists)
                {
                    // Username already exists, return false
                    return false;
                }

                // Username doesn't exist, proceed with user creation
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Error creating user: {ex.Message}");

                // If an exception occurs, return false
                return false;
            }
        }

    }
}
