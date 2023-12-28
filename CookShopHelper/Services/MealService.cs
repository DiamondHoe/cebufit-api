using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;

namespace CebuFitApi.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMapper _mapper;
        public MealService(IMealRepository mealRepository, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
        }

        public Task CreateMealAsync(MealDTO blogPostDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMealAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MealDTO>> GetAllMealsAsync()
        {
            var mealEntities = await _mealRepository.GetAllAsync();
            var mealDTOs = _mapper.Map<List<MealDTO>>(mealEntities);
            return mealDTOs;
        }

        public Task<MealDTO> GetMealByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMealAsync(int id, MealDTO blogPostDTO)
        {
            throw new NotImplementedException();
        }
    }
}
