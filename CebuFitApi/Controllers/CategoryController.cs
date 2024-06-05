using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CebuFitApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/categories")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IJwtTokenHelper _jwtTokenHelper;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IMapper mapper, IJwtTokenHelper jwtTokenHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _categoryService = categoryService;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpGet(Name = "GetCategories")]
        public async Task<ActionResult<CategoryDTO>> GetAll()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var categories = await _categoryService.GetAllCategoriesAsync(userIdClaim);
                if (categories.Count == 0)
                {
                    return NoContent();
                }
                return Ok(categories);
            }
            return NotFound("Username not found");
        }

        [HttpGet("{categoryId}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryDTO>> GetById(Guid categoryId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var category = await _categoryService.GetCategoryByIdAsync(categoryId, userIdClaim);

                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            return NotFound("User not found");
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                if (categoryCreateDTO == null || categoryCreateDTO.Name.IsNullOrEmpty())
                {
                    return BadRequest("Category data is null or name is empty.");
                }

                await _categoryService.CreateCategoryAsync(categoryCreateDTO, userIdClaim);

                return Ok();
            }
            return NotFound("User not found");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(CategoryDTO categoryDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                if(categoryDTO == null || categoryDTO.Name.IsNullOrEmpty())
                {
                    return BadRequest("Category data is null or name is empty.");
                }   

                var existingCategory = await _categoryService.GetCategoryByIdAsync(categoryDTO.Id, userIdClaim);

                if (existingCategory == null)
                {
                    return NotFound();
                }

                await _categoryService.UpdateCategoryAsync(categoryDTO, userIdClaim);

                return Ok();
            }
            return NotFound("User not found");
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteCategory(Guid categoryId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingCategory = await _categoryService.GetCategoryByIdAsync(categoryId, userIdClaim);

                if (existingCategory == null)
                {
                    return NotFound();
                }

                await _categoryService.DeleteCategoryAsync(categoryId, userIdClaim);

                return Ok();
            }
            return NotFound("User not found");
        }
    }
}
