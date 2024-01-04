using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;

namespace CebuFitApi.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        public MealService(IMealRepository mealRepository, IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<List<MealDTO>> GetAllMealsAsync()
        {
            var mealsEntities = await _mealRepository.GetAllAsync();
            var mealsDTOs = _mapper.Map<List<MealDTO>>(mealsEntities);
            return mealsDTOs;
        }

        public async Task<List<MealWithDetailsDTO>> GetAllMealsWithDetailsAsync()
        {
            var mealsEntities = await _mealRepository.GetAllWithDetailsAsync();
            var mealsDTOs = _mapper.Map<List<MealWithDetailsDTO>>(mealsEntities);
            return mealsDTOs;
        }

        public async Task<MealDTO> GetMealByIdAsync(Guid mealId)
        {
            var mealEntity = await _mealRepository.GetByIdAsync(mealId);
            var mealDTO = _mapper.Map<MealDTO>(mealEntity);
            return mealDTO;
        }

        public async Task<MealWithDetailsDTO> GetMealByIdWithDetailsAsync(Guid mealId)
        {
            var mealEntity = await _mealRepository.GetByIdWithDetailsAsync(mealId);
            var mealDTO = _mapper.Map<MealWithDetailsDTO>(mealEntity);
            return mealDTO;
        }

        public async Task CreateMealAsync(MealCreateDTO mealDTO)
        {
            var meal = _mapper.Map<Meal>(mealDTO);
            meal.Id = Guid.NewGuid();

            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (var ing in mealDTO.IngredientsId)
            {
                ingredients.Add(await _ingredientRepository.GetByIdAsync(ing));
            }
            meal.Ingredients = ingredients;

            await _mealRepository.CreateAsync(meal);
        }

        public async Task UpdateMealAsync(MealUpdateDTO mealDTO)
        {
            var meal = _mapper.Map<Meal>(mealDTO);

            List<Ingredient> ingredients = new List<Ingredient>();
            foreach(var ing in mealDTO.IngredientsId)
            {
                ingredients.Add(await _ingredientRepository.GetByIdAsync(ing));
            }
            meal.Ingredients = ingredients;

            await _mealRepository.UpdateAsync(meal);
        }

        public async Task DeleteMealAsync(Guid mealId)
        {
            await _mealRepository.DeleteAsync(mealId);
        }
    }
}
