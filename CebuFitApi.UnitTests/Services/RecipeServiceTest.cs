using CebuFitApi.Services;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
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
    [TestSubject(typeof(RecipeService))]
    public class RecipeServiceTest
    {
        private readonly Mock<IRecipeRepository> _recipeRepositoryMock;
        private readonly Mock<IIngredientRepository> _ingredientRepositoryMock;
        private readonly Mock<IIngredientService> _ingredientServiceMock;
        private readonly Mock<IStorageItemService> _storageItemServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly RecipeService _recipeService;

        public RecipeServiceTest()
        {
            _recipeRepositoryMock = new Mock<IRecipeRepository>();
            _ingredientRepositoryMock = new Mock<IIngredientRepository>();
            _ingredientServiceMock = new Mock<IIngredientService>();
            _storageItemServiceMock = new Mock<IStorageItemService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();

            _recipeService = new RecipeService(
                _mapperMock.Object,
                _recipeRepositoryMock.Object,
                _ingredientRepositoryMock.Object,
                _ingredientServiceMock.Object,
                _storageItemServiceMock.Object,
                _userRepositoryMock.Object
            );
        }

        [Fact]
        public async Task GetAllRecipesAsync_ShouldReturnRecipes()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var recipes = new List<Recipe> { new Recipe { Id = Guid.NewGuid() } };
            var recipeDTOs = new List<RecipeDTO> { new RecipeDTO { Id = recipes[0].Id } };

            _recipeRepositoryMock.Setup(repo => repo.GetAllAsync(userId)).ReturnsAsync(recipes);
            _mapperMock.Setup(mapper => mapper.Map<List<RecipeDTO>>(recipes)).Returns(recipeDTOs);

            // Act
            var result = await _recipeService.GetAllRecipesAsync(userId);

            // Assert
            Assert.Equal(recipeDTOs, result);
        }

        [Fact]
        public async Task GetAllRecipesWithDetailsAsync_ShouldReturnRecipesWithDetails()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dataType = DataType.Both;
            var recipes = new List<Recipe> { new Recipe { Id = Guid.NewGuid() } };
            var recipeWithDetailsDTOs = new List<RecipeWithDetailsDTO>
                { new RecipeWithDetailsDTO { Id = recipes[0].Id } };

            _recipeRepositoryMock.Setup(repo => repo.GetAllWithDetailsAsync(userId, dataType)).ReturnsAsync(recipes);
            _mapperMock.Setup(mapper => mapper.Map<List<RecipeWithDetailsDTO>>(recipes)).Returns(recipeWithDetailsDTOs);

            // Act
            var result = await _recipeService.GetAllRecipesWithDetailsAsync(userId, dataType);

            // Assert
            Assert.Equal(recipeWithDetailsDTOs, result);
        }

        [Fact]
        public async Task GetRecipeByIdAsync_ShouldReturnRecipe()
        {
            // Arrange
            var recipeId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var recipe = new Recipe { Id = recipeId };
            var recipeDTO = new RecipeDTO { Id = recipeId };

            _recipeRepositoryMock.Setup(repo => repo.GetByIdAsync(recipeId, userId)).ReturnsAsync(recipe);
            _mapperMock.Setup(mapper => mapper.Map<RecipeDTO>(recipe)).Returns(recipeDTO);

            // Act
            var result = await _recipeService.GetRecipeByIdAsync(recipeId, userId);

            // Assert
            Assert.Equal(recipeDTO, result);
        }

        [Fact]
        public async Task GetRecipeByIdWithDetailsAsync_ShouldReturnRecipeWithDetails()
        {
            // Arrange
            var recipeId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var recipe = new Recipe { Id = recipeId };
            var recipeWithDetailsDTO = new RecipeWithDetailsDTO { Id = recipeId };

            _recipeRepositoryMock.Setup(repo => repo.GetByIdWithDetailsAsync(recipeId, userId)).ReturnsAsync(recipe);
            _mapperMock.Setup(mapper => mapper.Map<RecipeWithDetailsDTO>(recipe)).Returns(recipeWithDetailsDTO);

            // Act
            var result = await _recipeService.GetRecipeByIdWithDetailsAsync(recipeId, userId);

            // Assert
            Assert.Equal(recipeWithDetailsDTO, result);
        }

        [Fact]
        public async Task CreateRecipeAsync_ShouldCreateRecipe()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var recipeCreateDTO = new RecipeCreateDTO
            {
                Name = "Test Recipe",
                Description = "Test Description",
                Ingredients = new List<IngredientCreateDTO>
                {
                    new IngredientCreateDTO { baseProductId = Guid.NewGuid() }
                }
            };
            var recipe = new Recipe
                { Id = Guid.NewGuid(), Name = recipeCreateDTO.Name, Description = recipeCreateDTO.Description };
            var user = new User { Id = userId };

            _mapperMock.Setup(mapper => mapper.Map<Recipe>(recipeCreateDTO)).Returns(recipe);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _ingredientServiceMock
                .Setup(service => service.CreateIngredientAsync(It.IsAny<IngredientCreateDTO>(), userId))
                .ReturnsAsync(Guid.NewGuid());
            _ingredientRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), userId))
                .ReturnsAsync(new Ingredient());

            // Act
            await _recipeService.CreateRecipeAsync(recipeCreateDTO, userId);

            // Assert
            _recipeRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Recipe>(), userId), Times.Once);
        }

        [Fact]
        public async Task UpdateRecipeAsync_ShouldUpdateRecipe()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var recipeUpdateDTO = new RecipeUpdateDTO
            {
                Id = Guid.NewGuid(),
                Name = "Updated Recipe",
                Description = "Updated Description",
                Ingredients = new List<IngredientCreateDTO>
                {
                    new IngredientCreateDTO { baseProductId = Guid.NewGuid() }
                }
            };
            var recipe = new Recipe
                { Id = recipeUpdateDTO.Id, Name = recipeUpdateDTO.Name, Description = recipeUpdateDTO.Description };
            var existingRecipe = new Recipe
            {
                Id = recipeUpdateDTO.Id, Ingredients = new List<Ingredient> { new Ingredient { Id = Guid.NewGuid() } }
            };

            _mapperMock.Setup(mapper => mapper.Map<Recipe>(recipeUpdateDTO)).Returns(recipe);
            _recipeRepositoryMock.Setup(repo => repo.GetByIdAsync(recipeUpdateDTO.Id, userId))
                .ReturnsAsync(existingRecipe);
            _ingredientServiceMock
                .Setup(service => service.CreateIngredientAsync(It.IsAny<IngredientCreateDTO>(), userId))
                .ReturnsAsync(Guid.NewGuid());
            _ingredientRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), userId))
                .ReturnsAsync(new Ingredient());

            // Act
            await _recipeService.UpdateRecipeAsync(recipeUpdateDTO, userId);

            // Assert
            _recipeRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Recipe>(), userId), Times.Once);
        }

        [Fact]
        public async Task DeleteRecipeAsync_ShouldDeleteRecipe()
        {
            // Arrange
            var recipeId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            await _recipeService.DeleteRecipeAsync(recipeId, userId);

            // Assert
            _recipeRepositoryMock.Verify(repo => repo.DeleteAsync(recipeId, userId), Times.Once);
        }

        [Fact]
        public async Task GetRecipesFromAvailableStorageItemsAsync_ShouldReturnRecipes()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var recipesCount = 5;
            var recipesWithDetails = new List<RecipeWithDetailsDTO>
            {
                new RecipeWithDetailsDTO { Id = Guid.NewGuid(), Ingredients = new List<IngredientWithProductDTO>() }
            };

            _storageItemServiceMock.Setup(service => service.GetAllStorageItemsWithProductAsync(userId, true))
                .ReturnsAsync(new List<StorageItemWithProductDTO>());
            _recipeRepositoryMock.Setup(repo => repo.GetAllWithDetailsAsync(userId, DataType.Both))
                .ReturnsAsync(new List<Recipe>());
            _mapperMock.Setup(mapper => mapper.Map<List<RecipeWithDetailsDTO>>(It.IsAny<List<Recipe>>()))
                .Returns(recipesWithDetails);

            // Act
            var result = await _recipeService.GetRecipesFromAvailableStorageItemsAsync(userId, recipesCount);

            // Assert
            Assert.NotNull(result);
        }
    }
}