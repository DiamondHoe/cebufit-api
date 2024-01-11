using CebuFitApi.Data;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CebuFitApi.Controllers
{
    [ApiController]
    [Route("/api/storages")]
    public class StorageController : Controller
    {
        private readonly ILogger<MealController> _logger;
        //private readonly IMealService _mealService;
        private readonly CebuFitApiDbContext _dbContext;
        public StorageController(CebuFitApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpDelete]
        public async Task ClearDataAsync()
        {
            var recipes = await _dbContext.Recipes.ToListAsync();
            _dbContext.Recipes.RemoveRange(recipes);

            var ingredients = await _dbContext.Ingredients.ToListAsync();
            _dbContext.Ingredients.RemoveRange(ingredients);

            await _dbContext.SaveChangesAsync();  // Wait for SaveChanges to complete
        }

    }
}
