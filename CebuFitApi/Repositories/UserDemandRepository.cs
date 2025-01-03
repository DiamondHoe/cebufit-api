﻿using CebuFitApi.Data;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class UserDemandRepository: IUserDemandRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public UserDemandRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDemand?> GetDemandAsync(Guid userId)
        {
            var demand = await _dbContext.UsersDemands
                .FirstOrDefaultAsync(x => x.User.Id == userId);
            return demand;
        }

        public async Task AddDemandAsync(UserDemand demand)
        {
            await _dbContext.UsersDemands.AddAsync(demand);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateDemandAsync(UserDemand demand, Guid userId)
        {
            var existingDemand = await _dbContext.UsersDemands
                .FirstOrDefaultAsync(x => x.User.Id == userId);
            if (existingDemand != null)
            {
                _dbContext.Entry(existingDemand).CurrentValues.SetValues(demand);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
