using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Models;
using CebuFitApi.Repositories;

namespace CebuFitApi.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;
        public RecipeService(IMapper mapper, IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository, IIngredientService ingredientService)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _ingredientService = ingredientService;

        }
        public async Task<List<RecipeDTO>> GetAllRecipesAsync()
        {
            var recipesEntities = await _recipeRepository.GetAllAsync();
            var storageItemsDTOs = _mapper.Map<List<RecipeDTO>>(recipesEntities);
            return storageItemsDTOs;
        }
        public async Task<List<RecipeWithDetailsDTO>> GetAllRecipesWithDetailsAsync()
        {
            var recipesEntities = await _recipeRepository.GetAllWithDetailsAsync();
            var recipesDTOs = _mapper.Map<List<RecipeWithDetailsDTO>>(recipesEntities);

            return recipesDTOs;
        }
        public async Task<RecipeDTO> GetRecipeByIdAsync(Guid recipeId)
        {
            var recipeEntity = await _recipeRepository.GetByIdAsync(recipeId);
            var recipeDto = _mapper.Map<RecipeDTO>(recipeEntity);
            return recipeDto;
        }
        public async Task<RecipeWithDetailsDTO> GetRecipeByIdWithDetailsAsync(Guid recipeId)
        {
            var recipeEntity = await _recipeRepository.GetByIdWithDetailsAsync(recipeId);
            var recipeDto = _mapper.Map<RecipeWithDetailsDTO>(recipeEntity);
            return recipeDto;
        }
        public async Task CreateRecipeAsync(RecipeCreateDTO recipeDTO)
        {
            var recipe = _mapper.Map<Recipe>(recipeDTO);
            recipe.Id = Guid.NewGuid();
            recipe.Ingredients.Clear();

            foreach(var ingredient in recipeDTO.Ingredients)
            {
                var ingredientId = await _ingredientService.CreateIngredientAsync(ingredient);
                recipe.Ingredients.Add(await _ingredientRepository.GetByIdAsync(ingredientId));
            }

            await _recipeRepository.CreateAsync(recipe);
        }
        public async Task UpdateRecipeAsync(RecipeUpdateDTO recipeDTO)
        {
            var recipe = _mapper.Map<Recipe>(recipeDTO);
            recipe.Ingredients.Clear();

            var existingRecipe = await _recipeRepository.GetByIdAsync(recipeDTO.Id);

            // Create tasks for adding new ingredients
            var addIngredientTasks = new List<Task<Ingredient>>();
            foreach (var ingredientDTO in recipeDTO.Ingredients)
            {
                var ingredientId = await _ingredientService.CreateIngredientAsync(ingredientDTO);
                var newIngredient = await _ingredientRepository.GetByIdAsync(ingredientId);
                addIngredientTasks.Add(Task.FromResult(newIngredient));
            }

            // Wait for all tasks to complete
            var addedIngredients = await Task.WhenAll(addIngredientTasks);

            // Add the new ingredients to the recipe
            recipe.Ingredients.AddRange(addedIngredients);

            // Delete existing ingredients using a copy of the list
            foreach (var ingredient in existingRecipe.Ingredients.ToList())
            {
                await _ingredientRepository.DeleteAsync(ingredient.Id);
            }

            // Update the recipe
            await _recipeRepository.UpdateAsync(recipe);
        }

        public async Task DeleteRecipeAsync(Guid recipeId)
        {
            await _recipeRepository.DeleteAsync(recipeId);
        }
    }
}
