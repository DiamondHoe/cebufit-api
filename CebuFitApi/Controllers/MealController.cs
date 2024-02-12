using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CebuFitApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/meals")]
    public class MealController : ControllerBase
    {
        private readonly ILogger<MealController> _logger;
        private readonly IMapper _mapper;
        private readonly IMealService _mealService;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public MealController(
            ILogger<MealController> logger,
            IMealService mealService,
            IMapper mapper,
            IJwtTokenHelper jwtTokenHelper)
        {
            _logger = logger;
            _mealService = mealService;
            _mapper = mapper;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpGet(Name = "GetMeals")]
        public async Task<ActionResult<List<MealDTO>>> GetAll()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var meals = await _mealService.GetAllMealsAsync(userIdClaim);
                if (meals.Count == 0)
                {
                    return NoContent();
                }
                return Ok(meals);
            }

            return NotFound("User not found");
        }

        [HttpGet("withDetails/", Name = "GetMealsWithDetails")]
        public async Task<ActionResult<List<MealWithDetailsDTO>>> GetAllWithDetails()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var meals = await _mealService.GetAllMealsWithDetailsAsync(userIdClaim);
                if (meals.Count == 0)
                {
                    return NoContent();
                }
                return Ok(meals);
            }

            return NotFound("User not found");
        }

        [HttpGet("{mealId}", Name = "GetMeal")]
        public async Task<ActionResult<List<MealDTO>>> GetById(Guid mealId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var meal = await _mealService.GetMealByIdAsync(mealId, userIdClaim);
                if (meal == null)
                {
                    return NotFound();
                }
                return Ok(meal);
            }

            return NotFound("User not found");
        }

        [HttpGet("withDetails/{mealId}", Name = "GetMealWithDetails")]
        public async Task<ActionResult<List<MealWithDetailsDTO>>> GetByIdWithDetails(Guid mealId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var meal = await _mealService.GetMealByIdWithDetailsAsync(mealId, userIdClaim);
                if (meal == null)
                {
                    return NotFound();
                }
                return Ok(meal);
            }

            return NotFound("User not found");
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateMeal(MealCreateDTO mealDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                if (mealDTO == null)
                {
                    return BadRequest("Meal data is null.");
                }

                var mealId = await _mealService.CreateMealAsync(mealDTO, userIdClaim);

                return Ok(mealId);
            }

            return NotFound("User not found");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMeal(MealUpdateDTO mealDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingMeal = await _mealService.GetMealByIdAsync(mealDTO.Id, userIdClaim);

                if (existingMeal == null)
                {
                    return NotFound();
                }

                await _mealService.UpdateMealAsync(mealDTO, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpDelete("{mealId}")]
        public async Task<ActionResult> DeleteMeal(Guid mealId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingMeal = await _mealService.GetMealByIdAsync(mealId, userIdClaim);

                if (existingMeal == null)
                {
                    return NotFound();
                }

                await _mealService.DeleteMealAsync(mealId, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }
        [HttpPut(Name = "PrepareMeal")]
        public async Task<ActionResult> PrepareMeal(MealPrepareDTO preparedMeal)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingMeal = await _mealService.GetMealByIdAsync(preparedMeal.Id, userIdClaim);

                if (existingMeal == null)
                {
                    return NotFound();
                }

                await _mealService.PrepareMealAsync(preparedMeal, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
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
