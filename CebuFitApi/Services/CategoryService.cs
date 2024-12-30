using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using CebuFitApi.Helpers.Enums;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDTO>> GetAllCategoriesAsync(Guid userIdClaim, DataType dataType)
    {
        var categoryEntities = await _categoryRepository.GetAllAsync(userIdClaim, dataType);
        var categoryDtos = _mapper.Map<List<CategoryDTO>>(categoryEntities);
        return categoryDtos;
    }

    public async Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId, Guid userIdClaim)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(categoryId, userIdClaim);
        var categoryDTO = _mapper.Map<CategoryDTO>(categoryEntity);
        return categoryDTO;
    }
    public async Task CreateCategoryAsync(CategoryCreateDTO categoryDTO, Guid userIdClaim)
    {
        var category = _mapper.Map<Category>(categoryDTO);
        category.Id = Guid.NewGuid();

        var foundUser = await _userRepository.GetByIdAsync(userIdClaim);
        if(foundUser != null)
        {
            category.User = foundUser;
            await _categoryRepository.AddAsync(category);
        }

    }

    public async Task UpdateCategoryAsync(CategoryDTO categoryDTO, Guid userIdClaim)
    {
        var category = _mapper.Map<Category>(categoryDTO);
        var foundUser = await _userRepository.GetByIdAsync(userIdClaim);
        if (foundUser != null)
        {
            await _categoryRepository.UpdateAsync(category, userIdClaim);
        }
    }

    public async Task DeleteCategoryAsync(Guid categoryId, Guid userIdClaim)
    {
        await _categoryRepository.DeleteAsync(categoryId, userIdClaim);
    }
}
