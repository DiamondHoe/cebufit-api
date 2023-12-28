using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IMapper mapper)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet(Name = "GetCategories")]
        public async Task<ActionResult<CategoryDTO>> Get()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            if (categories.Count == 0) 
            {
                return NoContent();
            }
            return Ok(categories);
        }
        [HttpGet("{categoryId}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryDTO>> GetById(Guid categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

    }
}
