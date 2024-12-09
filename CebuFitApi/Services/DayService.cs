using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.Helpers;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;

namespace CebuFitApi.Services
{
    public class DayService : IDayService
    {
        private readonly IDayRepository _dayRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserDemandService _userDemandService;
        private readonly IMapper _mapper;
        public DayService(IMapper mapper, IDayRepository dayRepository, IUserRepository userRepository, IUserDemandService userDemandService)
        {
            _mapper = mapper;
            _dayRepository = dayRepository;
            _userRepository = userRepository;
            _userDemandService = userDemandService;
        }
        public async Task<List<DayDTO>> GetAllDaysAsync(Guid userIdClaim)
        {
            var daysEntities = await _dayRepository.GetAllAsync(userIdClaim);
            var daysDTOs = _mapper.Map<List<DayDTO>>(daysEntities);
            return daysDTOs;
        }

        public async Task<List<DayWithMealsDTO>> GetAllDaysWithMealsAsync(Guid userIdClaim)
        {
            var daysEntities = await _dayRepository.GetAllWithMealsAsync(userIdClaim);
            var daysDTOs = _mapper.Map<List<DayWithMealsDTO>>(daysEntities);

            var userDemand = await _userDemandService.GetDemandAsync(userIdClaim);
            if(userDemand != null)
            {
                foreach (var day in daysDTOs)
                {
                    DemandHelper.CountDayDemand(day);
                    day.Demand.Calories = userDemand.Calories;
                    day.Demand.Carb = userDemand.Carb;
                    day.Demand.Fat = userDemand.Fat;
                    day.Demand.Protein = userDemand.Protein;
                }
            }
            return daysDTOs;
        }

        public async Task<DayDTO> GetDayByIdAsync(Guid dayId, Guid userIdClaim)
        {
            var dayEntity = await _dayRepository.GetByIdAsync(dayId, userIdClaim);
            var dayDTO = _mapper.Map<DayDTO>(dayEntity);
            return dayDTO;
        }

        public async Task<DayWithMealsDTO> GetDayByIdWithMealsAsync(Guid dayId, Guid userIdClaim)
        {
            var dayEntity = await _dayRepository.GetByIdWithMealsAsync(dayId, userIdClaim);
            var dayDTO = _mapper.Map<DayWithMealsDTO>(dayEntity);
            var userDemand = await _userDemandService.GetDemandAsync(userIdClaim);

            DemandHelper.CountDayDemand(dayDTO);
            dayDTO.Demand.Calories = userDemand.Calories;
            dayDTO.Demand.Carb = userDemand.Carb;
            dayDTO.Demand.Fat = userDemand.Fat;
            dayDTO.Demand.Protein = userDemand.Protein;

            return dayDTO;
        }
        
        public async Task<DayWithMealsDTO> GetDayByDateWithMealsAsync(DateTime date, Guid userIdClaim)
        {
            var dayEntity = await _dayRepository.GetByDateWithMealsAsync(date, userIdClaim);
            var dayDto = _mapper.Map<DayWithMealsDTO>(dayEntity);
            var userDemand = await _userDemandService.GetDemandAsync(userIdClaim);
            
            if(dayDto == null) return dayDto;
            DemandHelper.CountDayDemand(dayDto);
            dayDto.Demand.Calories = userDemand.Calories;
            dayDto.Demand.Carb = userDemand.Carb;
            dayDto.Demand.Fat = userDemand.Fat;
            dayDto.Demand.Protein = userDemand.Protein;

            return dayDto;
        }
        
        public async Task<Guid> CreateDayAsync(DayCreateDTO dayDTO, Guid userIdClaim)
        {
            var day = _mapper.Map<Day>(dayDTO);
            day.Id = Guid.NewGuid();
            var foundUser = await _userRepository.GetByIdAsync(userIdClaim);
            if (foundUser != null)
            {
                day.User = foundUser;
            }
            await _dayRepository.CreateAsync(day, userIdClaim);
            return day.Id;
        }

        public async Task UpdateDayAsync(DayUpdateDTO dayDto, Guid userIdClaim)
        {
            var day = _mapper.Map<Day>(dayDto);
            var foundUser = await _userRepository.GetByIdAsync(userIdClaim);
            if (foundUser != null)
            {
                await _dayRepository.CreateAsync(day, userIdClaim);
            }
        }
        public async Task DeleteDayAsync(Guid dayId, Guid userIdClaim)
        {
            await _dayRepository.DeleteAsync(dayId, userIdClaim);
        }

        #region Meals management in days
        public async Task<DayDTO> AddMealToDayAsync(Guid dayId, Guid mealId)
        {
            var dayEntity = await _dayRepository.AddMealToDayAsync(dayId, mealId);
            var dayDTO = _mapper.Map<DayDTO>(dayEntity);
            return dayDTO;
        }

        public async Task<DayDTO> RemoveMealFromDayAsync(Guid dayId, Guid mealId)
        {
            var dayEntity = await _dayRepository.RemoveMealFromDayAsync(dayId, mealId);
            var dayDTO = _mapper.Map<DayDTO>(dayEntity);
            return dayDTO;
        }

        public async Task<decimal?> GetCostsForDateRangeAsync(DateTime start, DateTime end, Guid userIdClaim)
        {
            decimal? costs = await _dayRepository.GetCostsForDateRangeAsync(start, end, userIdClaim);
            return costs;
        }

        public async Task<List<Day>> GetShoppingForDateRangeAsync(DateTime start, DateTime end, Guid userIdClaim)
        {
            var daysEntities = await _dayRepository.GetShoppingListForDateRangeAsync(start, end, userIdClaim);
            return daysEntities;
        }
        #endregion
    }
}
