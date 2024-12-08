using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CebuFitApi.Controllers;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CebuFitApi.UnitTests.Controllers
{
    [TestSubject(typeof(IngredientController))]
    public class IngredientControllerTest
    {
        private readonly Mock<ILogger<IngredientController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IIngredientService> _mockIngredientService;
        private readonly Mock<IJwtTokenHelper> _mockJwtTokenHelper;
        private readonly IngredientController _controller;

        public IngredientControllerTest()
        {
            _mockLogger = new Mock<ILogger<IngredientController>>();
            _mockMapper = new Mock<IMapper>();
            _mockIngredientService = new Mock<IIngredientService>();
            _mockJwtTokenHelper = new Mock<IJwtTokenHelper>();
            _controller = new IngredientController(
                _mockLogger.Object,
                _mockMapper.Object,
                _mockIngredientService.Object,
                _mockJwtTokenHelper.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WhenIngredientsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetAllIngredientsAsync(userId))
                .ReturnsAsync(new List<IngredientDTO> { new IngredientDTO() });

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<IngredientDTO>>(okResult.Value);
        }

        [Fact]
        public async Task GetAll_ReturnsNoContent_WhenNoIngredientsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetAllIngredientsAsync(userId))
                .ReturnsAsync(new List<IngredientDTO>());

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAllWithProduct_ReturnsOkResult_WhenIngredientsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetAllIngredientsWithProductAsync(userId))
                .ReturnsAsync(new List<IngredientWithProductDTO> { new IngredientWithProductDTO() });

            // Act
            var result = await _controller.GetAllWithProduct();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<IngredientWithProductDTO>>(okResult.Value);
        }

        [Fact]
        public async Task GetAllWithProduct_ReturnsNoContent_WhenNoIngredientsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetAllIngredientsWithProductAsync(userId))
                .ReturnsAsync(new List<IngredientWithProductDTO>());

            // Act
            var result = await _controller.GetAllWithProduct();

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetAllWithProduct_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetAllWithProduct();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenIngredientExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetIngredientByIdAsync(ingredientId, userId))
                .ReturnsAsync(new IngredientDTO());

            // Act
            var result = await _controller.GetById(ingredientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<IngredientDTO>(okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenIngredientDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetIngredientByIdAsync(ingredientId, userId))
                .ReturnsAsync((IngredientDTO)null);

            // Act
            var result = await _controller.GetById(ingredientId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetById(Guid.NewGuid());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetByIdWithProduct_ReturnsOkResult_WhenIngredientExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetIngredientByIdWithProductAsync(ingredientId, userId))
                .ReturnsAsync(new IngredientWithProductDTO());

            // Act
            var result = await _controller.GetByIdWithProduct(ingredientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<IngredientWithProductDTO>(okResult.Value);
        }

        [Fact]
        public async Task GetByIdWithProduct_ReturnsNotFound_WhenIngredientDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetIngredientByIdWithProductAsync(ingredientId, userId))
                .ReturnsAsync((IngredientWithProductDTO)null);

            // Act
            var result = await _controller.GetByIdWithProduct(ingredientId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetByIdWithProduct_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetByIdWithProduct(Guid.NewGuid());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task CreateIngredient_ReturnsOk_WhenIngredientCreated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientDTO = new IngredientCreateDTO();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.CreateIngredientAsync(ingredientDTO, userId))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _controller.CreateIngredient(ingredientDTO);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateIngredient_ReturnsBadRequest_WhenIngredientDTOIsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);

            // Act
            var result = await _controller.CreateIngredient(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Ingredient data is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateIngredient_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.CreateIngredient(new IngredientCreateDTO());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task UpdateIngredient_ReturnsOk_WhenIngredientUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientDTO = new IngredientDTO { Id = Guid.NewGuid() };
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetIngredientByIdAsync(ingredientDTO.Id, userId))
                .ReturnsAsync(new IngredientDTO());
            _mockIngredientService.Setup(x => x.UpdateIngredientAsync(ingredientDTO, userId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateIngredient(ingredientDTO);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateIngredient_ReturnsNotFound_WhenIngredientDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientDTO = new IngredientDTO { Id = Guid.NewGuid() };
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetIngredientByIdAsync(ingredientDTO.Id, userId))
                .ReturnsAsync((IngredientDTO)null);

            // Act
            var result = await _controller.UpdateIngredient(ingredientDTO);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateIngredient_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.UpdateIngredient(new IngredientDTO());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteIngredient_ReturnsOk_WhenIngredientDeleted()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetIngredientByIdAsync(ingredientId, userId))
                .ReturnsAsync(new IngredientDTO());
            _mockIngredientService.Setup(x => x.DeleteIngredientAsync(ingredientId, userId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteIngredient(ingredientId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteIngredient_ReturnsNotFound_WhenIngredientDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.GetIngredientByIdAsync(ingredientId, userId))
                .ReturnsAsync((IngredientDTO)null);

            // Act
            var result = await _controller.DeleteIngredient(ingredientId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteIngredient_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.DeleteIngredient(Guid.NewGuid());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task IsIngredientAvailable_ReturnsTrue_WhenIngredientIsAvailable()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientDTO = new IngredientCreateDTO();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.IsIngredientAvailable(ingredientDTO, userId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.IsIngredientAvailable(ingredientDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task IsIngredientAvailable_ReturnsFalse_WhenIngredientIsNotAvailable()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var ingredientDTO = new IngredientCreateDTO();
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mockIngredientService.Setup(x => x.IsIngredientAvailable(ingredientDTO, userId))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.IsIngredientAvailable(ingredientDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.False((bool)okResult.Value);
        }

        [Fact]
        public async Task IsIngredientAvailable_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.IsIngredientAvailable(new IngredientCreateDTO());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("User not found", notFoundResult.Value);
        }
    }
}