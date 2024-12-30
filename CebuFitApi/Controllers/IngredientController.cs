using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CebuFitApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/ingredients")]
    public class IngredientController : ControllerBase
    {
        private readonly ILogger<IngredientController> _logger;
        private readonly IMapper _mapper;
        private readonly IIngredientService _ingredientService;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public IngredientController(
            ILogger<IngredientController> logger,
            IMapper mapper,
            IIngredientService ingredientService,
            IJwtTokenHelper jwtTokenHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _ingredientService = ingredientService;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpGet(Name = "GetIngredients")]
        public async Task<ActionResult<List<IngredientDTO>>> GetAll()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var ingredients = await _ingredientService.GetAllIngredientsAsync(userIdClaim);
                if (ingredients.Count == 0)
                {
                    return NoContent();
                }
                return Ok(ingredients);
            }

            return NotFound("User not found");
        }

        [HttpGet("withProduct/", Name = "GetIngredientsWithProduct")]
        public async Task<ActionResult<List<IngredientWithProductDTO>>> GetAllWithProduct()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var ingredients = await _ingredientService.GetAllIngredientsWithProductAsync(userIdClaim);
                if (ingredients.Count == 0)
                {
                    return NoContent();
                }
                return Ok(ingredients);
            }

            return NotFound("User not found");
        }

        [HttpGet("{ingredientId}", Name = "GetIngredientById")]
        public async Task<ActionResult<IngredientDTO>> GetById(Guid ingredientId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var storageItem = await _ingredientService.GetIngredientByIdAsync(ingredientId, userIdClaim);
                if (storageItem == null)
                {
                    return NotFound();
                }
                return Ok(storageItem);
            }

            return NotFound("User not found");
        }

        [HttpGet("withProduct/{ingredientId}", Name = "GetIngredientByIdWithProduct")]
        public async Task<ActionResult<IngredientWithProductDTO>> GetByIdWithProduct(Guid ingredientId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var storageItem = await _ingredientService.GetIngredientByIdWithProductAsync(ingredientId, userIdClaim);
                if (storageItem == null)
                {
                    return NotFound();
                }
                return Ok(storageItem);
            }

            return NotFound("User not found");
        }

        [HttpPost]
        public async Task<ActionResult> CreateIngredient(IngredientCreateDTO ingredientDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                if (ingredientDTO == null)
                {
                    return BadRequest("Ingredient data is null.");
                }

                await _ingredientService.CreateIngredientAsync(ingredientDTO, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateIngredient(IngredientDTO ingredientDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingStorageItem = await _ingredientService.GetIngredientByIdAsync(ingredientDTO.Id, userIdClaim);

                if (existingStorageItem == null)
                {
                    return NotFound();
                }

                await _ingredientService.UpdateIngredientAsync(ingredientDTO, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpDelete("{ingredientId}")]
        public async Task<ActionResult> DeleteIngredient(Guid ingredientId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingStorageItem = await _ingredientService.GetIngredientByIdAsync(ingredientId, userIdClaim);

                if (existingStorageItem == null)
                {
                    return NotFound();
                }

                await _ingredientService.DeleteIngredientAsync(ingredientId, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpGet("IsIngredientAvailable", Name = "IsIngredientAvailable")]
        public async Task<ActionResult<bool>> IsIngredientAvailable(IngredientCreateDTO ingredientDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                return Ok((await _ingredientService.IsIngredientAvailable(ingredientDTO, userIdClaim)));
            }

            return NotFound("User not found");
        }
    }
}
