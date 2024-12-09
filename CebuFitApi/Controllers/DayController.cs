using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CebuFitApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/days")]
    public class DayController : ControllerBase
    {
        private readonly ILogger<DayController> _logger;
        private readonly IMapper _mapper;
        private readonly IDayService _dayService;
        private readonly IMealService _mealService;
        private readonly IJwtTokenHelper _jwtTokenHelper;
        private readonly IExcelHelper _excelHelper;

        public DayController(
            ILogger<DayController> logger,
            IDayService dayService,
            IMealService mealService,
            IMapper mapper,
            IJwtTokenHelper jwtTokenHelper,
            IExcelHelper excelHelper)
        {
            _logger = logger;
            _dayService = dayService;
            _mealService = mealService;
            _mapper = mapper;
            _jwtTokenHelper = jwtTokenHelper;
            _excelHelper = excelHelper;
        }

        [HttpGet(Name = "GetDays")]
        public async Task<ActionResult<List<DayDTO>>> GetAll()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var days = await _dayService.GetAllDaysAsync(userIdClaim);
                if (days.Count == 0)
                {
                    return NoContent();
                }
                return Ok(days);
            }

            return NotFound("User not found");
        }

        [HttpGet("withMeals/", Name = "GetDaysWithMeals")]
        public async Task<ActionResult<List<DayWithMealsDTO>>> GetAllWithMeals()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var days = await _dayService.GetAllDaysWithMealsAsync(userIdClaim);
                if (days.Count == 0)
                {
                    return NoContent();
                }
                return Ok(days);
            }

            return NotFound("User not found");
        }

        [HttpGet("{dayId}", Name = "GetDay")]
        public async Task<ActionResult<DayDTO>> GetById(Guid dayId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var day = await _dayService.GetDayByIdAsync(dayId, userIdClaim);
                if (day == null)
                {
                    return NotFound();
                }
                return Ok(day);
            }

            return NotFound("User not found");
        }

        [HttpGet("withMeals/{dayId}", Name = "GetDayWithMeals")]
        public async Task<ActionResult<DayWithMealsDTO>> GetByIdWithMeals([FromQuery] Guid dayId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var day = await _dayService.GetDayByIdWithMealsAsync(dayId, userIdClaim);
                if (day == null)
                {
                    return NotFound();
                }
                return Ok(day);
            }

            return NotFound("User not found");
        }
    
        [HttpGet("withMeals/byDate", Name = "GetDayByDateWithMeals")]
        public async Task<ActionResult<DayWithMealsDTO>> GetByDateWithMeals([FromQuery] DateTime date)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim != Guid.Empty)
            {
                var day = await _dayService.GetDayByDateWithMealsAsync(date, userIdClaim);
                if (day != null)
                {
                    return Ok(day);
                }
            }
            return NotFound("Day not found");
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateDay(DayCreateDTO dayDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                if (dayDTO == null)
                {
                    return BadRequest("Day data is null.");
                }

                var dayId = await _dayService.CreateDayAsync(dayDTO, userIdClaim);
                return Ok(dayId);
            }

            return NotFound("User not found");
        }

        [HttpPut(Name = "UpdateDay")]
        public async Task<ActionResult> UpdateDay(DayUpdateDTO dayDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingDay = await _dayService.GetDayByIdAsync(dayDTO.Id, userIdClaim);

                if (existingDay == null)
                {
                    return NotFound();
                }

                await _dayService.UpdateDayAsync(dayDTO, userIdClaim);
                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpDelete("{dayId}", Name = "DeleteDay")]
        public async Task<ActionResult> DeleteDay(Guid dayId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingDay = await _dayService.GetDayByIdAsync(dayId, userIdClaim);

                if (existingDay == null)
                {
                    return NotFound();
                }

                await _dayService.DeleteDayAsync(dayId, userIdClaim);
                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpPut("manageDayMeals/{dayId}", Name = "AddMealToDay")]
        public async Task<ActionResult<DayDTO>> AddMealToDay(Guid dayId, Guid mealId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingDay = await _dayService.GetDayByIdAsync(dayId, userIdClaim);
                var existingMeal = await _mealService.GetMealByIdAsync(mealId, userIdClaim);

                if (existingDay == null || existingMeal == null)
                {
                    return NotFound("Day or Meal not found");
                }

                var day = await _dayService.AddMealToDayAsync(dayId, mealId);
                return Ok(day);
            }

            return NotFound("User not found");
        }

        [HttpDelete("manageDayMeals", Name = "RemoveMealFromDay")]
        public async Task<ActionResult> RemoveMealFromDay([FromQuery] Guid dayId, [FromQuery] Guid mealId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingDay = await _dayService.GetDayByIdAsync(dayId, userIdClaim);
                var existingMeal = await _mealService.GetMealByIdAsync(mealId, userIdClaim);

                if (existingDay == null || existingMeal == null)
                {
                    return NotFound("Day or Meal not found");
                }

                await _dayService.RemoveMealFromDayAsync(dayId, mealId);
                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpGet("GetCostsForDateRange")]
        public async Task<ActionResult<decimal?>> GetCostsForDateRange(DateTime start, DateTime end)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var costs = await _dayService.GetCostsForDateRangeAsync(start, end, userIdClaim);
                return Ok(costs);
            }

            return NotFound("User not found");
        }

        [HttpGet("GetShoppingListForDateRange")]
        public async Task<ActionResult> GetShoppingList(DateTime start, DateTime end)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var days = await _dayService.GetShoppingForDateRangeAsync(start, end, userIdClaim);

                try
                {
                    var excelBytes = await _excelHelper.GenerateExcel(days);
                    return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "shopping_list.xlsx");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }

            return NotFound("User not found");
        }
    }
}
