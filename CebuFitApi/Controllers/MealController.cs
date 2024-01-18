using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CebuFitApi.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<Guid>> CreateMeal(MealCreateDTO mealDTO)
        {
            if (mealDTO == null)
            {
                return BadRequest("Meal data is null.");
            }

            var mealid = await _mealService.CreateMealAsync(mealDTO);

            return Ok(mealid);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMeal(MealUpdateDTO mealDTO)
        {
            var existingMeal = await _mealService.GetMealByIdAsync(mealDTO.Id);

            if (existingMeal == null)
            {
                return NotFound();
            }

            await _mealService.UpdateMealAsync(mealDTO);

            return Ok();
        }
        [HttpDelete("{mealId}")]
        public async Task<ActionResult> DeleteMeal(Guid mealId)
        {
            var existingMeal = await _mealService.GetMealByIdAsync(mealId);

            if (existingMeal == null)
            {
                return NotFound();
            }

            await _mealService.DeleteMealAsync(mealId);

            return Ok();
        }

        [HttpGet("mealTimes", Name = "GetMealTimes")]
        public async Task<ActionResult<Dictionary<string, int>>> GetMealTimes()
        {
            Dictionary<string, int> mealTimeDict = new Dictionary<string, int>();
            var mealValues = Enum.GetValues(typeof(MealTimesEnum));

            foreach (var value in mealValues)
            {
                // Assuming the enum values are strings, you can convert them to string
                var stringValue = value.ToString();

                // Assign each enum value to a corresponding key in the dictionary
                mealTimeDict[stringValue] = (int)value;
            }

            return Ok(mealTimeDict);
        }
    }
}
