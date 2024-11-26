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
        public async Task<User> GetByIdAsync(Guid userIdclaim)
        {
            var foundUser = await _dbContext.Users
                .Include(user => user.Demand)
                .FirstOrDefaultAsync(user => user.Id == userIdclaim);
                
            return foundUser;
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            var foundUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
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

        public async Task UpdateAsync(User user)
        {
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            // TODO np
            if (existingUser != null)
            {
                existingUser.Weight = user.Weight;
                _dbContext.Entry(existingUser).CurrentValues.SetValues(existingUser);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<bool> DeleteAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

    }
}
