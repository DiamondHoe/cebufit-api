using JetBrains.Annotations;
using Moq;
using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CebuFitApi.Helpers.Enums;
using Xunit;

namespace CebuFitApi.UnitTests.Services
{
    [TestSubject(typeof(CategoryService))]
    public class CategoryServiceTest
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryService _categoryService;

        public CategoryServiceTest()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _categoryService = new CategoryService(_mockCategoryRepository.Object, _mockUserRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnCategoryDTOs()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dataType = DataType.Both;
            var categories = new List<Category> { new Category { Id = Guid.NewGuid(), Name = "Category1" } };
            _mockCategoryRepository.Setup(repo => repo.GetAllAsync(userId, dataType)).ReturnsAsync(categories);
            _mockMapper.Setup(m => m.Map<List<CategoryDTO>>(categories)).Returns(new List<CategoryDTO>
                { new CategoryDTO { Id = categories[0].Id, Name = categories[0].Name } });

            // Act
            var result = await _categoryService.GetAllCategoriesAsync(userId, dataType);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(categories[0].Id, result[0].Id);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ShouldReturnCategoryDTO()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Name = "Category1" };
            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(categoryId, userId)).ReturnsAsync(category);
            _mockMapper.Setup(m => m.Map<CategoryDTO>(category))
                .Returns(new CategoryDTO { Id = category.Id, Name = category.Name });

            // Act
            var result = await _categoryService.GetCategoryByIdAsync(categoryId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoryId, result.Id);
        }

        [Fact]
        public async Task CreateCategoryAsync_ShouldAddCategory()
        {
            // Arrange
            var categoryCreateDto = new CategoryCreateDTO { Name = "New Category" };
            var userId = Guid.NewGuid();
            var user = new User { Id = userId };
            var category = new Category { Id = Guid.NewGuid(), Name = categoryCreateDto.Name, User = user };
            _mockMapper.Setup(m => m.Map<Category>(categoryCreateDto)).Returns(category);
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            await _categoryService.CreateCategoryAsync(categoryCreateDto, userId);

            // Assert
            _mockCategoryRepository.Verify(
                repo => repo.AddAsync(It.Is<Category>(c => c.Name == categoryCreateDto.Name)), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldUpdateCategory()
        {
            // Arrange
            var categoryDto = new CategoryDTO { Id = Guid.NewGuid(), Name = "Updated Category" };
            var userId = Guid.NewGuid();
            var user = new User { Id = userId };
            var category = new Category { Id = categoryDto.Id, Name = categoryDto.Name, User = user };
            _mockMapper.Setup(m => m.Map<Category>(categoryDto)).Returns(category);
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            await _categoryService.UpdateCategoryAsync(categoryDto, userId);

            // Assert
            _mockCategoryRepository.Verify(
                repo => repo.UpdateAsync(It.Is<Category>(c => c.Name == categoryDto.Name), userId), Times.Once);
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldDeleteCategory()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            await _categoryService.DeleteCategoryAsync(categoryId, userId);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.DeleteAsync(categoryId, userId), Times.Once);
        }
    }
}