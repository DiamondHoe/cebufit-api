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
        private readonly IStorageItemRepository _storageItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public MealService(
            IMealRepository mealRepository,
            IIngredientService ingredientService,
            IIngredientRepository ingredientRepository,
            IStorageItemService storageItemService,
            IUserRepository userRepository,
            IStorageItemRepository storageItemRepository,
            IMapper mapper)
        {
            _mealRepository = mealRepository;
            _ingredientService = ingredientService;
            _ingredientRepository = ingredientRepository;
            _storageItemService = storageItemService;
            _storageItemRepository = storageItemRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<MealDTO>> GetAllMealsAsync(Guid userIdClaim)
        {
            var mealsEntities = await _mealRepository.GetAllAsync(userIdClaim);
            var mealsDTOs = _mapper.Map<List<MealDTO>>(mealsEntities);
            return mealsDTOs;
        }

        public async Task<List<MealWithDetailsDTO>> GetAllMealsWithDetailsAsync(Guid userIdClaim)
        {
            var mealsEntities = await _mealRepository.GetAllWithDetailsAsync(userIdClaim);
            var mealsDTOs = _mapper.Map<List<MealWithDetailsDTO>>(mealsEntities);

            foreach (var meal in mealsDTOs)
            {
                meal.Doable = await AreIngredientsAvailable(meal.Ingredients, userIdClaim);
            }

            return mealsDTOs;
        }

        public async Task<MealDTO> GetMealByIdAsync(Guid mealId, Guid userIdClaim)
        {
            var mealEntity = await _mealRepository.GetByIdAsync(mealId, userIdClaim);
            var mealDTO = _mapper.Map<MealDTO>(mealEntity);
            return mealDTO;
        }

        public async Task<MealWithDetailsDTO> GetMealByIdWithDetailsAsync(Guid mealId, Guid userIdClaim)
        {
            var mealEntity = await _mealRepository.GetByIdWithDetailsAsync(mealId, userIdClaim);
            var mealDTO = _mapper.Map<MealWithDetailsDTO>(mealEntity);
            mealDTO.Doable = await AreIngredientsAvailable(mealDTO.Ingredients, userIdClaim);
            return mealDTO;
        }

        public async Task<Guid> CreateMealAsync(MealCreateDTO mealDTO, Guid userIdClaim)
        {
            var meal = _mapper.Map<Meal>(mealDTO);
            meal.Id = Guid.NewGuid();

            var foundUser = await _userRepository.GetById(userIdClaim);
            if (foundUser != null)
            {
                meal.User = foundUser;

                meal.Ingredients.Clear();

                var ingredients = new List<Ingredient>();
                foreach (var ing in mealDTO.Ingredients)
                {
                    var ingredientId = await _ingredientService.CreateIngredientAsync(ing, userIdClaim);
                    ingredients.Add(await _ingredientRepository.GetByIdAsync(ingredientId, userIdClaim));
                }
                meal.Ingredients = ingredients;

                var mealID = await _mealRepository.CreateAsync(meal, userIdClaim);
                return mealID;
            }
            return Guid.Empty;
        }

        public async Task UpdateMealAsync(MealUpdateDTO mealDTO, Guid userIdClaim)
        {
            var meal = _mapper.Map<Meal>(mealDTO);
            meal.Ingredients.Clear();

            var existingMeal = await _mealRepository.GetByIdAsync(mealDTO.Id, userIdClaim);

            // Create tasks for adding new ingredients
            var addIngredientTasks = new List<Task<Ingredient>>();
            foreach (var ingredientDTO in mealDTO.Ingredients)
            {
                var ingredientId = await _ingredientService.CreateIngredientAsync(ingredientDTO, userIdClaim);
                var newIngredient = await _ingredientRepository.GetByIdAsync(ingredientId, userIdClaim);
                addIngredientTasks.Add(Task.FromResult(newIngredient));
            }

            // Wait for all tasks to complete
            var addedIngredients = await Task.WhenAll(addIngredientTasks);

            // Add the new ingredients to the meal
            meal.Ingredients.AddRange(addedIngredients);

            // Delete existing ingredients using a copy of the list
            foreach (var ingredient in existingMeal.Ingredients.ToList())
            {
                await _ingredientRepository.DeleteAsync(ingredient.Id, userIdClaim);
            }

            // Update the meal
            await _mealRepository.UpdateAsync(meal, userIdClaim);
        }



        public async Task DeleteMealAsync(Guid mealId, Guid userIdClaim)
        {
            var foundUser = await _userRepository.GetById(userIdClaim);
            if (foundUser != null)
            {
                await _mealRepository.DeleteAsync(mealId, userIdClaim);
            }
        }

        private async Task<bool> AreIngredientsAvailable(List<IngredientWithProductDTO> ingredients, Guid userIdClaim)
        {
            var storageItemsDTOs = await _storageItemService.GetAllStorageItemsWithProductAsync(userIdClaim);

            return ingredients.All(ingredient =>
                (ingredient.Quantity.HasValue || ingredient.Weight.HasValue) &&
                storageItemsDTOs.Any(storageItem =>
                    storageItem.Product.Id == ingredient.Product.Id &&
                    (ingredient.Quantity.HasValue
                        ? storageItem.Quantity >= ingredient.Quantity
                        : storageItem.Weight >= ingredient.Weight)));
        }

        public async Task PrepareMealAsync(MealPrepareDTO mealPrepareDTO, Guid userIdClaim)
        {
            var existingMeal = await _mealRepository.GetByIdAsync(mealPrepareDTO.Id, userIdClaim);
            if (existingMeal != null)
            {
                foreach (var storageItemPrepare in mealPrepareDTO.StorageItems)
                {
                    var foundSi = await _storageItemService.GetStorageItemByIdAsync(storageItemPrepare.Id, userIdClaim);

                    if (foundSi != null)
                    {
                        if (storageItemPrepare.Quantity != null && foundSi.Quantity != null)
                        {
                            var substracted = foundSi.Quantity - storageItemPrepare.Quantity;

                            if (substracted <= 0)
                            {
                                foundSi.Prepared = true;
                            }

                            if (substracted > 0)
                            {
                                foundSi.Quantity = substracted;
                                foundSi.Prepared = false;

                                StorageItemCreateDTO leftSi = new StorageItemCreateDTO();
                                leftSi.expirationDate = foundSi.expirationDate;
                                leftSi.Price = foundSi.Price;
                                leftSi.Quantity = foundSi.Quantity - substracted;
                                leftSi.baseProductId = (await _storageItemService.GetStorageItemByIdWithProductAsync(storageItemPrepare.Id, userIdClaim)).Product.Id;

                                await _storageItemService.CreateStorageItemAsync(leftSi, userIdClaim);
                            }

                            await _storageItemService.UpdateStorageItemAsync(foundSi, userIdClaim);
                        }

                        if (storageItemPrepare.Weight != null && foundSi.Weight != null)
                        {
                            var substracted = foundSi.Weight - storageItemPrepare.Weight;

                            if (substracted <= 0)
                            {
                                foundSi.Prepared = true;
                            }

                            if (substracted > 0)
                            {
                                foundSi.Weight = substracted;
                                foundSi.Prepared = false;

                                StorageItemCreateDTO leftSi = new StorageItemCreateDTO();
                                leftSi.expirationDate = foundSi.expirationDate;
                                leftSi.Price = foundSi.Price;
                                leftSi.Weight = foundSi.Weight - substracted;
                                leftSi.baseProductId = (await _storageItemService.GetStorageItemByIdWithProductAsync(storageItemPrepare.Id, userIdClaim)).Product.Id;

                                await _storageItemService.CreateStorageItemAsync(leftSi, userIdClaim);
                            }

                            await _storageItemService.UpdateStorageItemAsync(foundSi, userIdClaim);
                        }
                    }
                }
            }

        }
    }
}
