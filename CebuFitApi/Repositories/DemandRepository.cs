using CebuFitApi.Data;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Repositories
{
    public class DemandRepository: IDemandRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public DemandRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Demand> GetDemandAsync(Guid userId)
        {
            var demand = await _dbContext.Demands
                .FirstOrDefaultAsync(x => x.User.Id == userId);
            return demand;
        }

        public async Task UpdateDemandAsync(Demand demand, Guid userId)
        {
            var exisitingDemand = await _dbContext.Demands
                .FirstOrDefaultAsync(x => x.User.Id == userId);
            if (exisitingDemand != null)
            {
                _dbContext.Entry(exisitingDemand).CurrentValues.SetValues(demand);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
