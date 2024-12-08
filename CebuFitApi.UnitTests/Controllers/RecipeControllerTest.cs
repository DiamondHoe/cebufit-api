using CebuFitApi.Controllers;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Xunit;

namespace CebuFitApi.UnitTests.Controllers;

[TestSubject(typeof(RecipeController))]
public class RecipeControllerTest
{
    private readonly Mock<ILogger<RecipeController>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRecipeService> _recipeServiceMock;
    private readonly Mock<IJwtTokenHelper> _jwtTokenHelperMock;
    private readonly RecipeController _controller;

    public RecipeControllerTest()
    {
        _loggerMock = new Mock<ILogger<RecipeController>>();
        _mapperMock = new Mock<IMapper>();
        _recipeServiceMock = new Mock<IRecipeService>();
        _jwtTokenHelperMock = new Mock<IJwtTokenHelper>();
        _controller = new RecipeController(
            _loggerMock.Object,
            _mapperMock.Object,
            _recipeServiceMock.Object,
            _jwtTokenHelperMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfRecipes()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetAllRecipesAsync(userId))
            .ReturnsAsync(new List<RecipeDTO> { new RecipeDTO() });

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<RecipeDTO>>(okResult.Value);
    }

    [Fact]
    public async Task GetAll_ReturnsNoContent_WhenNoRecipesFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetAllRecipesAsync(userId)).ReturnsAsync(new List<RecipeDTO>());

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task GetAllWithDetails_ReturnsOkResult_WithListOfRecipesWithDetails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetAllRecipesWithDetailsAsync(userId, DataType.Both))
            .ReturnsAsync(new List<RecipeWithDetailsDTO> { new RecipeWithDetailsDTO() });

        // Act
        var result = await _controller.GetAllWithDetails();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<RecipeWithDetailsDTO>>(okResult.Value);
    }

    [Fact]
    public async Task GetAllWithDetails_ReturnsNoContent_WhenNoRecipesFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetAllRecipesWithDetailsAsync(userId, DataType.Both))
            .ReturnsAsync(new List<RecipeWithDetailsDTO>());

        // Act
        var result = await _controller.GetAllWithDetails();

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task GetAllWithDetails_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetAllWithDetails();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithRecipe()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetRecipeByIdAsync(recipeId, userId)).ReturnsAsync(new RecipeDTO());

        // Act
        var result = await _controller.GetById(recipeId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<RecipeDTO>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenRecipeNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetRecipeByIdAsync(recipeId, userId)).ReturnsAsync((RecipeDTO)null);

        // Act
        var result = await _controller.GetById(recipeId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetById(recipeId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task GetByIdWithDetails_ReturnsOkResult_WithRecipeWithDetails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetRecipeByIdWithDetailsAsync(recipeId, userId))
            .ReturnsAsync(new RecipeWithDetailsDTO());

        // Act
        var result = await _controller.GetByIdWithDetails(recipeId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<RecipeWithDetailsDTO>(okResult.Value);
    }

    [Fact]
    public async Task GetByIdWithDetails_ReturnsNotFound_WhenRecipeNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetRecipeByIdWithDetailsAsync(recipeId, userId))
            .ReturnsAsync((RecipeWithDetailsDTO)null);

        // Act
        var result = await _controller.GetByIdWithDetails(recipeId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetByIdWithDetails_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetByIdWithDetails(recipeId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task CreateRecipe_ReturnsOk_WhenRecipeCreated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipeDTO = new RecipeCreateDTO();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);

        // Act
        var result = await _controller.CreateRecipe(recipeDTO);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task CreateRecipe_ReturnsBadRequest_WhenRecipeDTOIsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);

        // Act
        var result = await _controller.CreateRecipe(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Recipe data is null.", badRequestResult.Value);
    }

    [Fact]
    public async Task CreateRecipe_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var recipeDTO = new RecipeCreateDTO();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.CreateRecipe(recipeDTO);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task UpdateRecipe_ReturnsOk_WhenRecipeUpdated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipeDTO = new RecipeUpdateDTO { Id = Guid.NewGuid() };
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetRecipeByIdAsync(recipeDTO.Id, userId)).ReturnsAsync(new RecipeDTO());

        // Act
        var result = await _controller.UpdateRecipe(recipeDTO);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateRecipe_ReturnsNotFound_WhenRecipeNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipeDTO = new RecipeUpdateDTO { Id = Guid.NewGuid() };
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetRecipeByIdAsync(recipeDTO.Id, userId)).ReturnsAsync((RecipeDTO)null);

        // Act
        var result = await _controller.UpdateRecipe(recipeDTO);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateRecipe_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var recipeDTO = new RecipeUpdateDTO { Id = Guid.NewGuid() };
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.UpdateRecipe(recipeDTO);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task DeleteRecipe_ReturnsOk_WhenRecipeDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetRecipeByIdAsync(recipeId, userId)).ReturnsAsync(new RecipeDTO());

        // Act
        var result = await _controller.DeleteRecipe(recipeId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteRecipe_ReturnsNotFound_WhenRecipeNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetRecipeByIdAsync(recipeId, userId)).ReturnsAsync((RecipeDTO)null);

        // Act
        var result = await _controller.DeleteRecipe(recipeId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteRecipe_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.DeleteRecipe(recipeId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task GetAvailableRecipes_ReturnsOkResult_WithListOfAvailableRecipes()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipesCount = 5;
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetRecipesFromAvailableStorageItemsAsync(userId, recipesCount)).ReturnsAsync(
            new List<Tuple<RecipeWithDetailsDTO, List<Tuple<IngredientWithProductDTO, Tuple<decimal?, decimal?>>>>>
            {
                new Tuple<RecipeWithDetailsDTO, List<Tuple<IngredientWithProductDTO, Tuple<decimal?, decimal?>>>>(
                    new RecipeWithDetailsDTO(), new List<Tuple<IngredientWithProductDTO, Tuple<decimal?, decimal?>>>())
            });

        // Act
        var result = await _controller.GetAvailableRecipes(recipesCount);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert
            .IsType<
                List<Tuple<RecipeWithDetailsDTO, List<Tuple<IngredientWithProductDTO, Tuple<decimal?, decimal?>>>>>>(
                okResult.Value);
    }

    [Fact]
    public async Task GetAvailableRecipes_ReturnsNoContent_WhenNoAvailableRecipesFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var recipesCount = 5;
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(userId);
        _recipeServiceMock.Setup(s => s.GetRecipesFromAvailableStorageItemsAsync(userId, recipesCount)).ReturnsAsync(
            new List<Tuple<RecipeWithDetailsDTO, List<Tuple<IngredientWithProductDTO, Tuple<decimal?, decimal?>>>>>());

        // Act
        var result = await _controller.GetAvailableRecipes(recipesCount);

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task GetAvailableRecipes_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var recipesCount = 5;
        _jwtTokenHelperMock.Setup(j => j.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetAvailableRecipes(recipesCount);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found", notFoundResult.Value);
    }
}