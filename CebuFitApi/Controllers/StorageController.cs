using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [ApiController]
    [Route("/api/storages")]
    public class StorageController : Controller
    {
        private readonly ILogger<MealController> _logger;
        //private readonly IMealService _mealService;
        public StorageController()
        {
                
        }
    }
}
