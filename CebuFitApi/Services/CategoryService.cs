using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CategoryService : ICategoryRepository
{
    private readonly CebuFitApi.Interfaces.ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(CebuFitApi.Interfaces.ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
    {
        var categoryEntities = await _categoryRepository.GetAllAsync();
        var categoryDTOs = _mapper.Map<List<CategoryDTO>>(categoryEntities);
        return categoryDTOs;
    }

    public async Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(categoryId);
        var categoryDTO = _mapper.Map<CategoryDTO>(categoryEntity);
        return categoryDTO;
    }
    public async Task CreateCategoryAsync(CategoryCreateDTO categoryDTO)
    {
        var category = _mapper.Map<Category>(categoryDTO);
        category.Id = Guid.NewGuid();
        await _categoryRepository.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
    {
        var category = _mapper.Map<Category>(categoryDTO);
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteCategoryAsync(Guid categoryId)
    {
        await _categoryRepository.DeleteAsync(categoryId);
    }
}
