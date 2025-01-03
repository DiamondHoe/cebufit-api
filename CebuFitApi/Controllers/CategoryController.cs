﻿using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<CategoryDTO>> GetAll(DataType dataType = DataType.Both)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim == Guid.Empty) return NotFound("Username not found");
            
            var categories = await _categoryService.GetAllCategoriesAsync(userIdClaim, dataType);
            if (categories.Count == 0) return NoContent();
            return Ok(categories);
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
                if (categoryCreateDTO == null)
                {
                    return BadRequest("Category data is null.");
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
