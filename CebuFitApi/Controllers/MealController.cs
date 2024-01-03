using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Services;
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
        private readonly IMapper _mapper;
        private readonly IMealService _mealService;
        public MealController(ILogger<MealController> logger, IMealService mealService, IMapper mapper)
        {
            _logger = logger;
            _mealService = mealService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetMeals")]
        public async Task<ActionResult<List<MealDTO>>> GetAll()
        {
            var meals = await _mealService.GetAllMealsAsync();
            if (meals.Count == 0)
            {
                return NoContent();
            }
            return Ok(meals);
        }
        //[HttpGet("{mealId}", Name = "GetProduct")]
        //public async Task<ActionResult<List<MealDTO>>> GetById(Guid mealId)
        //{
        //    //var product = await _mealService.GetProductByIdAsync(productId);
        //    //if (product == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    //return Ok(product);
        //}
    }
}
