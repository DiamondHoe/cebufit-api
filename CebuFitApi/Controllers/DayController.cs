using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [ApiController]
    [Route("/api/days")]
    public class DayController : Controller
    {
        private readonly ILogger<DayController> _logger;
        private readonly IMapper _mapper;
        private readonly IDayService _dayService;
        private readonly IMealService _mealService;
        public DayController(ILogger<DayController> logger, IDayService dayService, IMealService mealService, IMapper mapper)
        {
            _logger = logger;
            _dayService = dayService;
            _mealService = mealService;
            _mapper = mapper;
        }
        [HttpGet(Name = "GetDays")]
        public async Task<ActionResult<List<DayDTO>>> GetAll()
        {
            var days = await _dayService.GetAllDaysAsync();
            if (days.Count == 0)
            {
                return NoContent();
            }
            return Ok(days);
        }
        [HttpGet("withMeals/", Name = "GetDaysWithMeals")]
        public async Task<ActionResult<List<DayWithMealsDTO>>> GetAllWithMeals()
        {
            var days = await _dayService.GetAllDaysWithMealsAsync();
            if (days.Count == 0)
            {
                return NoContent();
            }
            return Ok(days);
        }
        [HttpGet("{dayId}", Name = "GetDay")]
        public async Task<ActionResult<DayDTO>> GetById(Guid dayId)
        {
            var day = await _dayService.GetDayByIdAsync(dayId);
            if (day == null)
            {
                return NotFound();
            }
            return Ok(day);
        }

        [HttpGet("withMeals/{dayId}", Name = "GetDayWithMeals")]
        public async Task<ActionResult<DayWithMealsDTO>> GetByIdWithMeals(Guid dayId)
        {
            var day = await _dayService.GetDayByIdWithMealsAsync(dayId);
            if (day == null)
            {
                return NotFound();
            }
            return Ok(day);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateDay(DayCreateDTO dayDTO)
        {
            if (dayDTO == null)
            {
                return BadRequest("Day data is null.");
            }

            var dayId = await _dayService.CreateDayAsync(dayDTO);

            return Ok(dayId);
        }
        [HttpPut(Name = "UpdateDay")]
        public async Task<ActionResult> UpdateDay(DayUpdateDTO dayDTO)
        {
            var existingDay = await _dayService.GetDayByIdAsync(dayDTO.Id);

            if (existingDay == null)
            {
                return NotFound();
            }

            await _dayService.UpdateDayAsync(dayDTO);

            return Ok();
        }
        [HttpDelete("{dayId}", Name = "DeleteDay")]
        public async Task<ActionResult> DeleteDay(Guid dayId)
        {
            var existingDay = await _dayService.GetDayByIdAsync(dayId);

            if (existingDay == null)
            {
                return NotFound();
            }

            await _dayService.DeleteDayAsync(dayId);

            return Ok();
        }

        #region Meals management in days
        [HttpPut("manageDayMeals/{dayId}", Name = "AddMealToDay")]
        public async Task<ActionResult<DayDTO>> AddMealToDay(Guid dayId, Guid mealId)
        {
            var existingDay = await _dayService.GetDayByIdAsync(dayId);
            var existingMeal = await _mealService.GetMealByIdAsync(mealId);

            if (existingDay == null || existingMeal == null)
            {
                return NotFound("Day or Meal not found");
            }

            var day = await _dayService.AddMealToDayAsync(dayId, mealId);
            return Ok(day);
        }
        [HttpDelete("manageDayMeals/{dayId}", Name = "RemoveMealFromDay")]
        public async Task<ActionResult> RemoveMealFromDay(Guid dayId, Guid mealId)
        {
            var existingDay = await _dayService.GetDayByIdAsync(dayId);
            var existingMeal = await _mealService.GetMealByIdAsync(mealId);

            if (existingDay == null || existingMeal == null)
            {
                return NotFound("Day or Meal not found");
            }

            await _dayService.RemoveMealFromDayAsync(dayId, mealId);
            return Ok();
        }
        #endregion
    }
}
