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
        [HttpGet("withDetails/", Name = "GetMealsWithDetails")]
        public async Task<ActionResult<List<MealWithDetailsDTO>>> GetAllWithDetails()
        {
            var meals = await _mealService.GetAllMealsWithDetailsAsync();
            if (meals.Count == 0)
            {
                return NoContent();
            }
            return Ok(meals);
        }
        [HttpGet("{mealId}", Name = "GetMeal")]
        public async Task<ActionResult<List<MealDTO>>> GetById(Guid mealId)
        {
            var meal = await _mealService.GetMealByIdAsync(mealId);
            if (meal == null)
            {
                return NotFound();
            }
            return Ok(meal);
        }
        [HttpGet("withDetails/{mealId}", Name = "GetMealWithDetails")]
        public async Task<ActionResult<List<MealWithDetailsDTO>>> GetByIdWithDetails(Guid mealId)
        {
            var meal = await _mealService.GetMealByIdWithDetailsAsync(mealId);
            if (meal == null)
            {
                return NotFound();
            }
            return Ok(meal);
        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct(MealCreateDTO mealDTO)
        {
            if (mealDTO == null)
            {
                return BadRequest("Product data is null.");
            }

            await _mealService.CreateMealAsync(mealDTO);

            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMeal(MealUpdateDTO mealDTO)
        {
            var existingProduct = await _mealService.GetMealByIdAsync(mealDTO.Id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            await _mealService.UpdateMealAsync(mealDTO);

            return Ok();
        }
        [HttpDelete("{mealId}")]
        public async Task<ActionResult> DeleteProduct(Guid mealId)
        {
            var existingMeal = await _mealService.GetMealByIdAsync(mealId);

            if (existingMeal == null)
            {
                return NotFound();
            }

            await _mealService.DeleteMealAsync(mealId);

            return Ok();
        }
    }
}
