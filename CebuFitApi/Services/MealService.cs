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
        private readonly IIngredientService _ingredientService;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IStorageItemService _storageItemService;
        private readonly IMapper _mapper;
        public MealService(
            IMealRepository mealRepository,
            IIngredientService ingredientService,
            IIngredientRepository ingredientRepository,
            IStorageItemService storageItemRepository,
            IMapper mapper)
        {
            _mealRepository = mealRepository;
            _ingredientService = ingredientService;
            _ingredientRepository = ingredientRepository;
            _storageItemService = storageItemRepository;
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

            foreach (var meal in mealsDTOs)
            {
                meal.Doable = await AreIngredientsAvailable(meal.Ingredients);
            }

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
            mealDTO.Doable = await AreIngredientsAvailable(mealDTO.Ingredients);
            return mealDTO;
        }

        public async Task CreateMealAsync(MealCreateDTO mealDTO)
        {
            var meal = _mapper.Map<Meal>(mealDTO);
            meal.Id = Guid.NewGuid();
            meal.Ingredients.Clear();

            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (var ing in mealDTO.Ingredients)
            {
                var ingredientId = await _ingredientService.CreateIngredientAsync(ing);
                ingredients.Add(await _ingredientRepository.GetByIdAsync(ingredientId));
            }
            meal.Ingredients = ingredients;

            await _mealRepository.CreateAsync(meal);
        }

        public async Task UpdateMealAsync(MealUpdateDTO mealDTO)
        {
            var meal = _mapper.Map<Meal>(mealDTO);

            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (var ing in mealDTO.IngredientsId)
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

        private async Task<bool> AreIngredientsAvailable(List<IngredientWithProductDTO> ingredients)
        {
            var storageItemsDTOs = await _storageItemService.GetAllStorageItemsWithProductAsync();

            return ingredients.All(ingredient =>
                (ingredient.Quantity.HasValue || ingredient.Weight.HasValue) &&
                storageItemsDTOs.Any(storageItem =>
                    storageItem.Product.Id == ingredient.Product.Id &&
                    (ingredient.Quantity.HasValue
                        ? storageItem.Quantity >= ingredient.Quantity
                        : storageItem.Weight >= ingredient.Weight)));
        }
    }
}
