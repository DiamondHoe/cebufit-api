using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class DayService : IDayService
    {
        private readonly IDayRepository _dayRepository;
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;
        public DayService(IMapper mapper, IDayRepository dayRepository, IMealService mealService)
        {
            _mapper = mapper;
            _dayRepository = dayRepository;
            _mealService = mealService;
        }
        public async Task<List<DayDTO>> GetAllDaysAsync()
        {
            var daysEntities = await _dayRepository.GetAllAsync();
            var daysDTOs = _mapper.Map<List<DayDTO>>(daysEntities);
            return daysDTOs;
        }

        public async Task<List<DayWithMealsDTO>> GetAllDaysWithMealsAsync()
        {
            var daysEntities = await _dayRepository.GetAllWithMealsAsync();
            var daysDTOs = _mapper.Map<List<DayWithMealsDTO>>(daysEntities);
            return daysDTOs;
        }

        public async Task<DayDTO> GetDayByIdAsync(Guid dayId)
        {
            var dayEntity = await _dayRepository.GetByIdAsync(dayId);
            var dayDTO = _mapper.Map<DayDTO>(dayEntity);
            return dayDTO;
        }

        public async Task<DayWithMealsDTO> GetDayByIdWithMealsAsync(Guid dayId)
        {
            var dayEntity = await _dayRepository.GetByIdWithMealsAsync(dayId);
            var dayDTO = _mapper.Map<DayWithMealsDTO>(dayEntity);
            return dayDTO;
        }
        public async Task<Guid> CreateDayAsync(DayCreateDTO dayDTO)
        {
            var day = _mapper.Map<Day>(dayDTO);
            day.Id = Guid.NewGuid();
            await _dayRepository.CreateAsync(day);
            return day.Id;
        }

        public async Task UpdateDayAsync(DayUpdateDTO dayDto)
        {
            var day = _mapper.Map<Day>(dayDto);
            await _dayRepository.CreateAsync(day);
        }
        public async Task DeleteDayAsync(Guid dayId)
        {
            await _dayRepository.DeleteAsync(dayId);
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
        #endregion
    }
}
