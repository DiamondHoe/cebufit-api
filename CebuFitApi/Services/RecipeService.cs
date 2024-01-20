using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;

namespace CebuFitApi.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IIngredientService _ingredientService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public RecipeService(IMapper mapper, IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository, IIngredientService ingredientService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _ingredientService = ingredientService;
            _userRepository = userRepository;
        }
        public async Task<List<RecipeDTO>> GetAllRecipesAsync(Guid userIdClaim)
        {
            var recipesEntities = await _recipeRepository.GetAllAsync(userIdClaim);
            var storageItemsDTOs = _mapper.Map<List<RecipeDTO>>(recipesEntities);
            return storageItemsDTOs;
        }
        public async Task<List<RecipeWithDetailsDTO>> GetAllRecipesWithDetailsAsync(Guid userIdClaim)
        {
            var recipesEntities = await _recipeRepository.GetAllWithDetailsAsync(userIdClaim);
            var recipesDTOs = _mapper.Map<List<RecipeWithDetailsDTO>>(recipesEntities);

            return recipesDTOs;
        }
        public async Task<RecipeDTO> GetRecipeByIdAsync(Guid recipeId, Guid userIdClaim)
        {
            var recipeEntity = await _recipeRepository.GetByIdAsync(recipeId, userIdClaim);
            var recipeDto = _mapper.Map<RecipeDTO>(recipeEntity);
            return recipeDto;
        }
        public async Task<RecipeWithDetailsDTO> GetRecipeByIdWithDetailsAsync(Guid recipeId, Guid userIdClaim)
        {
            var recipeEntity = await _recipeRepository.GetByIdWithDetailsAsync(recipeId, userIdClaim);
            var recipeDto = _mapper.Map<RecipeWithDetailsDTO>(recipeEntity);
            return recipeDto;
        }
        public async Task CreateRecipeAsync(RecipeCreateDTO recipeDTO, Guid userIdClaim)
        {
            var recipe = _mapper.Map<Recipe>(recipeDTO);
            recipe.Id = Guid.NewGuid();

            var foundUser = await _userRepository.GetById(userIdClaim);
            if (foundUser != null)
            {
                recipe.User = foundUser;

                recipe.Ingredients.Clear();

                foreach (var ingredient in recipeDTO.Ingredients)
                {
                    var ingredientId = await _ingredientService.CreateIngredientAsync(ingredient, userIdClaim);
                    recipe.Ingredients.Add(await _ingredientRepository.GetByIdAsync(ingredientId, userIdClaim));
                }

                await _recipeRepository.CreateAsync(recipe, userIdClaim);
            }
        }
        public async Task UpdateRecipeAsync(RecipeUpdateDTO recipeDTO, Guid userIdClaim)
        {
            var recipe = _mapper.Map<Recipe>(recipeDTO);
            recipe.Ingredients.Clear();

            var existingRecipe = await _recipeRepository.GetByIdAsync(recipeDTO.Id, userIdClaim);

            // Create tasks for adding new ingredients
            var addIngredientTasks = new List<Task<Ingredient>>();
            foreach (var ingredientDTO in recipeDTO.Ingredients)
            {
                var ingredientId = await _ingredientService.CreateIngredientAsync(ingredientDTO, userIdClaim);
                var newIngredient = await _ingredientRepository.GetByIdAsync(ingredientId, userIdClaim);
                addIngredientTasks.Add(Task.FromResult(newIngredient));
            }

            // Wait for all tasks to complete
            var addedIngredients = await Task.WhenAll(addIngredientTasks);

            // Add the new ingredients to the recipe
            recipe.Ingredients.AddRange(addedIngredients);

            // Delete existing ingredients using a copy of the list
            foreach (var ingredient in existingRecipe.Ingredients.ToList())
            {
                await _ingredientRepository.DeleteAsync(ingredient.Id, userIdClaim);
            }

            // Update the recipe
            await _recipeRepository.UpdateAsync(recipe, userIdClaim);
        }

        public async Task DeleteRecipeAsync(Guid recipeId, Guid userIdClaim)
        {
            await _recipeRepository.DeleteAsync(recipeId, userIdClaim);
        }
    }
}
