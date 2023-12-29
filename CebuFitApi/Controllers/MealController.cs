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
        private readonly IMapper _mapper;
        public MealController(ILogger<MealController> logger, IMealService mealService, IMapper mapper)
        {           
            _logger = logger;
             _mealService = mealService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetMeals")]
        public async Task<List<MealDTO>> Get()
        {
            return await _mealService.GetAllMealsAsync();
        }
    }
}
