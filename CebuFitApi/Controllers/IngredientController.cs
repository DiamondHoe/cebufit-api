using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [ApiController]
    [Route("/api/ingredients")]
    public class IngredientController : Controller
    {
        private readonly ILogger<IngredientController> _logger;
        private readonly IMapper _mapper;
        private readonly IIngredientService _ingredientService;
        public IngredientController(ILogger<IngredientController> logger, IMapper mapper, IIngredientService ingredientService)
        {
            _logger = logger;
            _mapper = mapper;
            _ingredientService = ingredientService;
        }

        [HttpGet(Name = "GetIngredients")]
        public async Task<ActionResult<List<IngredientDTO>>> GetAll()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            if (ingredients.Count == 0)
            {
                return NoContent();
            }
            return Ok(ingredients);
        }

        [HttpGet("withProduct/", Name = "GetIngredientsWithProduct")]
        public async Task<ActionResult<List<IngredientWithProductDTO>>> GetAllWithProduct()
        {
            var ingredients = await _ingredientService.GetAllIngredientsWithProductAsync();
            if (ingredients.Count == 0)
            {
                return NoContent();
            }
            return Ok(ingredients);
        }

        [HttpGet("{ingredientId}", Name = "GetIngredientById")]
        public async Task<ActionResult<IngredientDTO>> GetById(Guid ingredientId)
        {
            var storageItem = await _ingredientService.GetIngredientByIdAsync(ingredientId);
            if (storageItem == null)
            {
                return NotFound();
            }
            return Ok(storageItem);
        }
        [HttpGet("withProduct/{ingredientId}", Name = "GetIngredientByIdWithProduct")]
        public async Task<ActionResult<IngredientWithProductDTO>> GetByIdWithProduct(Guid ingredientId)
        {
            var storageItem = await _ingredientService.GetIngredientByIdWithProductAsync(ingredientId);
            if (storageItem == null)
            {
                return NotFound();
            }
            return Ok(storageItem);
        }
        [HttpPost]
        public async Task<ActionResult> CreateIngredient(IngredientCreateDTO IngredientDTO)
        {
            if (IngredientDTO == null)
            {
                return BadRequest("Product data is null.");
            }

            await _ingredientService.CreateIngredientAsync(IngredientDTO);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateIngredient(IngredientDTO IngredientDTO)
        {
            var existingStorageItem = await _ingredientService.GetIngredientByIdAsync(IngredientDTO.Id);

            if (existingStorageItem == null)
            {
                return NotFound();
            }

            await _ingredientService.UpdateIngredientAsync(IngredientDTO);

            return Ok();
        }

        [HttpDelete("{ingredientId}")]
        public async Task<ActionResult> DeleteIngredient(Guid ingredientId)
        {
            var existingStorageItem = await _ingredientService.GetIngredientByIdAsync(ingredientId);

            if (existingStorageItem == null)
            {
                return NotFound();
            }

            await _ingredientService.DeleteIngredientAsync(ingredientId);

            return Ok();
        }
        [HttpGet("IsIngredientAvailable", Name = "IsIngredientAvailable")]
        public async Task<ActionResult<bool>> IsIngredientAvailable(IngredientCreateDTO ingredientDTO)
        {
            return await _ingredientService.IsIngredientAvailable(ingredientDTO);
        }
    }
}
