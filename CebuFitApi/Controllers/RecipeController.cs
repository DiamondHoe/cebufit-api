using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Models;
using CebuFitApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [ApiController]
    [Route("/api/recipes")]
    public class RecipeController : Controller
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IMapper _mapper;
        private readonly IRecipeService _recipeService;
        public RecipeController(ILogger<RecipeController> logger, IMapper mapper, IRecipeService recipeService)
        {
            _logger = logger;
            _mapper = mapper;
            _recipeService = recipeService;
        }
        [HttpGet(Name = "GetRecipes")]
        public async Task<ActionResult<List<RecipeDTO>>> GetAll()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            if(recipes.Count == 0)
            {
                return NoContent();
            }
            return Ok(recipes);
        }
        [HttpGet("withDetails/", Name = "GetRecipesWithDetails")]
        public async Task<ActionResult<List<RecipeWithDetailsDTO>>> GetAllWithDetails()
        {
            var recipes = await _recipeService.GetAllRecipesWithDetailsAsync();
            if (recipes.Count == 0)
            {
                return NoContent();
            }
            return Ok(recipes);
        }
        [HttpGet("{recipeId}", Name = "GetRecipeById")]
        public async Task<ActionResult<RecipeDTO>> GetById(Guid recipeId)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(recipeId);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }
        [HttpGet("withDetails/{recipeId}", Name = "GetRecipeByIdWithDetails")]
        public async Task<ActionResult<RecipeWithDetailsDTO>> GetByIdWithDetails(Guid recipeId)
        {
            var recipe = await _recipeService.GetRecipeByIdWithDetailsAsync(recipeId);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }
        [HttpPost]
        public async Task<ActionResult> CreateRecipe(RecipeCreateDTO recipeDTO)
        {
            if (recipeDTO == null)
            {
                return BadRequest("Product data is null.");
            }

            await _recipeService.CreateRecipeAsync(recipeDTO);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(RecipeDTO recipeDTO)
        {
            var existingRecipe = await _recipeService.GetRecipeByIdAsync(recipeDTO.Id);

            if (existingRecipe == null)
            {
                return NotFound();
            }

            await _recipeService.UpdateRecipeAsync(recipeDTO);

            return Ok();
        }

        [HttpDelete("{recipeId}")]
        public async Task<ActionResult> DeleteProduct(Guid recipeId)
        {
            var existingRecipe = await _recipeService.GetRecipeByIdAsync(recipeId);

            if (existingRecipe == null)
            {
                return NotFound();
            }

            await _recipeService.DeleteRecipeAsync(recipeId);

            return Ok();
        }
    }
}
