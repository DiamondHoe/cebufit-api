using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CebuFitApi.Controllers
{    
    [ApiController]
    [Route("/api/meals")]
    public class MealController : Controller
    {
        private readonly ILogger<MealController> _logger;
        private readonly IMealService _mealService;
        public MealController(ILogger<MealController> logger, IMealService mealService)
        {           
            _logger = logger;
             _mealService = mealService;
        }

        [HttpGet(Name = "GetMeals")]
        public async Task<List<MealDTO>> Get()
        {
            return await _mealService.GetAllMealsAsync();
        }
    }
}
