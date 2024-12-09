using CebuFitApi.Services;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Xunit;

namespace CebuFitApi.UnitTests.Services
{
    [TestSubject(typeof(IngredientService))]
    public class IngredientServiceTest
    {
        private readonly Mock<IIngredientRepository> _ingredientRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IStorageItemService> _storageItemServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IngredientService _ingredientService;

        public IngredientServiceTest()
        {
            _ingredientRepositoryMock = new Mock<IIngredientRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _storageItemServiceMock = new Mock<IStorageItemService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();

            _ingredientService = new IngredientService(
                _mapperMock.Object,
                _ingredientRepositoryMock.Object,
                _productRepositoryMock.Object,
                _storageItemServiceMock.Object,
                _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllIngredientsAsync_ShouldReturnListOfIngredientDTOs()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredients = new List<Ingredient> { new Ingredient() };
            var ingredientDTOs = new List<IngredientDTO> { new IngredientDTO() };

            _ingredientRepositoryMock.Setup(repo => repo.GetAllAsync(userId)).ReturnsAsync(ingredients);
            _mapperMock.Setup(mapper => mapper.Map<List<IngredientDTO>>(ingredients)).Returns(ingredientDTOs);

            // Act
            var result = await _ingredientService.GetAllIngredientsAsync(userId);

            // Assert
            Assert.Equal(ingredientDTOs, result);
        }

        [Fact]
        public async Task GetAllIngredientsWithProductAsync_ShouldReturnListOfIngredientWithProductDTOs()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredients = new List<Ingredient> { new Ingredient() };
            var ingredientDTOs = new List<IngredientWithProductDTO> { new IngredientWithProductDTO() };

            _ingredientRepositoryMock.Setup(repo => repo.GetAllWithProductAsync(userId)).ReturnsAsync(ingredients);
            _mapperMock.Setup(mapper => mapper.Map<List<IngredientWithProductDTO>>(ingredients))
                .Returns(ingredientDTOs);

            // Act
            var result = await _ingredientService.GetAllIngredientsWithProductAsync(userId);

            // Assert
            Assert.Equal(ingredientDTOs, result);
        }

        [Fact]
        public async Task GetIngredientByIdAsync_ShouldReturnIngredientDTO()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientId = Guid.NewGuid();
            var ingredient = new Ingredient();
            var ingredientDTO = new IngredientDTO();

            _ingredientRepositoryMock.Setup(repo => repo.GetByIdAsync(ingredientId, userId)).ReturnsAsync(ingredient);
            _mapperMock.Setup(mapper => mapper.Map<IngredientDTO>(ingredient)).Returns(ingredientDTO);

            // Act
            var result = await _ingredientService.GetIngredientByIdAsync(ingredientId, userId);

            // Assert
            Assert.Equal(ingredientDTO, result);
        }

        [Fact]
        public async Task GetIngredientByIdWithProductAsync_ShouldReturnIngredientWithProductDTO()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientId = Guid.NewGuid();
            var ingredient = new Ingredient();
            var ingredientDTO = new IngredientWithProductDTO();

            _ingredientRepositoryMock.Setup(repo => repo.GetByIdWithProductAsync(ingredientId, userId))
                .ReturnsAsync(ingredient);
            _mapperMock.Setup(mapper => mapper.Map<IngredientWithProductDTO>(ingredient)).Returns(ingredientDTO);

            // Act
            var result = await _ingredientService.GetIngredientByIdWithProductAsync(ingredientId, userId);

            // Assert
            Assert.Equal(ingredientDTO, result);
        }

        [Fact]
        public async Task CreateIngredientAsync_ShouldReturnNewIngredientId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientDTO = new IngredientCreateDTO { baseProductId = Guid.NewGuid() };
            var ingredient = new Ingredient();
            var user = new User();
            var product = new Product();

            _mapperMock.Setup(mapper => mapper.Map<Ingredient>(ingredientDTO)).Returns(ingredient);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(ingredientDTO.baseProductId, userId))
                .ReturnsAsync(product);
            _ingredientRepositoryMock.Setup(repo => repo.CreateAsync(ingredient, userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _ingredientService.CreateIngredientAsync(ingredientDTO, userId);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task UpdateIngredientAsync_ShouldCallUpdateOnRepository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientDTO = new IngredientDTO();
            var ingredient = new Ingredient();
            var user = new User();

            _mapperMock.Setup(mapper => mapper.Map<Ingredient>(ingredientDTO)).Returns(ingredient);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _ingredientRepositoryMock.Setup(repo => repo.UpdateAsync(ingredient, userId)).Returns(Task.CompletedTask);

            // Act
            await _ingredientService.UpdateIngredientAsync(ingredientDTO, userId);

            // Assert
            _ingredientRepositoryMock.Verify(repo => repo.UpdateAsync(ingredient, userId), Times.Once);
        }

        [Fact]
        public async Task DeleteIngredientAsync_ShouldCallDeleteOnRepository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientId = Guid.NewGuid();
            var user = new User();

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _ingredientRepositoryMock.Setup(repo => repo.DeleteAsync(ingredientId, userId)).Returns(Task.CompletedTask);

            // Act
            await _ingredientService.DeleteIngredientAsync(ingredientId, userId);

            // Assert
            _ingredientRepositoryMock.Verify(repo => repo.DeleteAsync(ingredientId, userId), Times.Once);
        }

        [Fact]
        public async Task IsIngredientAvailable_ShouldReturnTrueIfAvailable()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientDTO = new IngredientCreateDTO { baseProductId = Guid.NewGuid(), Quantity = 5 };
            var storageItems = new List<StorageItemWithProductDTO>
            {
                new StorageItemWithProductDTO
                    { Product = new ProductWithMacroDTO { Id = ingredientDTO.baseProductId }, ActualQuantity = 10 }
            };

            _storageItemServiceMock.Setup(service => service.GetAllStorageItemsWithProductAsync(userId, false))
                .ReturnsAsync(storageItems);

            // Act
            var result = await _ingredientService.IsIngredientAvailable(ingredientDTO, userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsIngredientAvailable_ShouldReturnFalseIfNotAvailable()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientDTO = new IngredientCreateDTO { baseProductId = Guid.NewGuid(), Quantity = 15 };
            var storageItems = new List<StorageItemWithProductDTO>
            {
                new StorageItemWithProductDTO
                    { Product = new ProductWithMacroDTO { Id = ingredientDTO.baseProductId }, ActualQuantity = 10 }
            };

            _storageItemServiceMock.Setup(service => service.GetAllStorageItemsWithProductAsync(userId, false))
                .ReturnsAsync(storageItems);

            // Act
            var result = await _ingredientService.IsIngredientAvailable(ingredientDTO, userId);

            // Assert
            Assert.False(result);
        }
    }
}