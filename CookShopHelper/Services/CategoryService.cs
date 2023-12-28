using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryDTO> CreateCategoryAsync(Category category)
    {
        // Add asynchronous implementation for creating a new category
        // Example: await _categoryRepository.AddAsync(category);
        throw new NotImplementedException();
    }

    public async Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(categoryId);
        var categoryDTO = _mapper.Map<CategoryDTO>(categoryEntity);
        return categoryDTO;
    }

    public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
    {
        var categoryEntities = await _categoryRepository.GetAllAsync();
        var categoryDTOs = _mapper.Map<List<CategoryDTO>>(categoryEntities);
        return categoryDTOs;
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        // Add asynchronous implementation for updating an existing category
        // Example: await _categoryRepository.UpdateAsync(category);
        throw new NotImplementedException();
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        // Add asynchronous implementation for deleting a category by ID
        // Example: await _categoryRepository.DeleteAsync(categoryId);
        throw new NotImplementedException();
    }
}
