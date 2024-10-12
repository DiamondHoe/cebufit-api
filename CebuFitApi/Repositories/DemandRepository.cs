using CebuFitApi.Data;
using CebuFitApi.Interfaces;

namespace CebuFitApi.Repositories
{
    public class DemandRepository: IDemandRepository
    {
        private readonly CebuFitApiDbContext _dbContext;
        public DemandRepository(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
