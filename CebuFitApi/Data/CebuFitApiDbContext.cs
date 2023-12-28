using CebuFitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Data
{
    public class CebuFitApiDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public CebuFitApiDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }
        public DbSet<Meal> Meals { get; set; }
    }
}
