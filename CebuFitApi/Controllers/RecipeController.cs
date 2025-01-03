﻿using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Helpers;

namespace CebuFitApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/recipes")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IMapper _mapper;
        private readonly IRecipeService _recipeService;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public RecipeController(
            ILogger<RecipeController> logger,
            IMapper mapper,
            IRecipeService recipeService,
            IJwtTokenHelper jwtTokenHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _recipeService = recipeService;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpGet(Name = "GetRecipes")]
        public async Task<ActionResult<List<RecipeDTO>>> GetAll()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var recipes = await _recipeService.GetAllRecipesAsync(userIdClaim);
                if (recipes.Count == 0)
                {
                    return NoContent();
                }
                return Ok(recipes);
            }

            return NotFound("User not found");
        }

        [HttpGet("withDetails/", Name = "GetRecipesWithDetails")]
        public async Task<ActionResult<List<RecipeWithDetailsDTO>>> GetAllWithDetails(
            [FromQuery] DataType dataType = DataType.Both)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var recipes = await _recipeService.GetAllRecipesWithDetailsAsync(userIdClaim, dataType);
                if (recipes.Count == 0)
                {
                    return NoContent();
                }
                return Ok(recipes);
            }

            return NotFound("User not found");
        }

        [HttpGet("{recipeId}", Name = "GetRecipeById")]
        public async Task<ActionResult<RecipeDTO>> GetById(Guid recipeId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var recipe = await _recipeService.GetRecipeByIdAsync(recipeId, userIdClaim);
                if (recipe == null)
                {
                    return NotFound();
                }
                return Ok(recipe);
            }

            return NotFound("User not found");
        }

        [HttpGet("withDetails/{recipeId}", Name = "GetRecipeByIdWithDetails")]
        public async Task<ActionResult<RecipeWithDetailsDTO>> GetByIdWithDetails(Guid recipeId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var recipe = await _recipeService.GetRecipeByIdWithDetailsAsync(recipeId, userIdClaim);
                if (recipe == null)
                {
                    return NotFound();
                }
                return Ok(recipe);
            }

            return NotFound("User not found");
        }

        [HttpPost]
        public async Task<ActionResult> CreateRecipe(RecipeCreateDTO recipeDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                if (recipeDTO == null)
                {
                    return BadRequest("Recipe data is null.");
                }

                await _recipeService.CreateRecipeAsync(recipeDTO, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRecipe(RecipeUpdateDTO recipeDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingRecipe = await _recipeService.GetRecipeByIdAsync(recipeDTO.Id, userIdClaim);

                if (existingRecipe == null)
                {
                    return NotFound();
                }

                await _recipeService.UpdateRecipeAsync(recipeDTO, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpDelete("{recipeId}")]
        public async Task<ActionResult> DeleteRecipe(Guid recipeId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingRecipe = await _recipeService.GetRecipeByIdAsync(recipeId, userIdClaim);

                if (existingRecipe == null)
                {
                    return NotFound();
                }

                await _recipeService.DeleteRecipeAsync(recipeId, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpGet("available/{recipesCount}", Name = "GetAvailableRecipes")]
        public async Task<ActionResult<List<RecipeDetail>>> GetAvailableRecipes(int recipesCount)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim != Guid.Empty)
            {
                var meals = await _recipeService.GetRecipesFromAvailableStorageItemsAsync(userIdClaim, recipesCount);
                if (meals.Count == 0)
                {
                    return NoContent();
                }
                var recipeDetails = meals.Select(meal => new RecipeDetail
                {
                    Recipe = meal.Item1,
                    Ingredients = meal.Item2.Select(ingredientTuple => new IngredientDetail
                    {
                        Ingredient = ingredientTuple.Item1,
                        Quantity = ingredientTuple.Item2.Item1,
                        Weight = ingredientTuple.Item2.Item2
                    }).ToList()
                }).ToList();
                return Ok(recipeDetails);
            }
            return NotFound("User not found");
        }
    }
}
