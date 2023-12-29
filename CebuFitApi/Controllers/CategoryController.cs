﻿using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet(Name = "GetCategories")]
        public async Task<ActionResult<CategoryDTO>> GetAll()
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
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            if (categoryCreateDTO == null)
            {
                return BadRequest("Category data is null.");
            }

            var createdCategory = await _categoryService.CreateCategoryAsync(categoryCreateDTO);

            if (createdCategory == null)
            {
                return StatusCode(500, "A problem occurred while handling your request.");
            }

            var categoryDTO = _mapper.Map<CategoryDTO>(createdCategory);

            return CreatedAtRoute("GetCategoryById", new { categoryId = categoryDTO.Id, }, categoryDTO);
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteCategory(Guid categoryId)
        {
            var existingCategory = await _categoryService.GetCategoryByIdAsync(categoryId);

            if (existingCategory == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategoryAsync(categoryId);

            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(CategoryDTO categoryDTO)
        {
            var existingCategory = await _categoryService.GetCategoryByIdAsync(categoryDTO.Id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            await _categoryService.UpdateCategoryAsync(categoryDTO);

            return Ok(categoryDTO);
        }

    }
}
