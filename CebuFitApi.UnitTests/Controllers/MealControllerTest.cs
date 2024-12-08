using CebuFitApi.Controllers;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CebuFitApi.UnitTests.Controllers
{
    [TestSubject(typeof(MealController))]
    public class MealControllerTest
    {
        private readonly Mock<IMealService> _mealServiceMock;
        private readonly Mock<IJwtTokenHelper> _jwtTokenHelperMock;
        private readonly Mock<ILogger<MealController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MealController _mealController;

        public MealControllerTest()
        {
            _mealServiceMock = new Mock<IMealService>();
            _jwtTokenHelperMock = new Mock<IJwtTokenHelper>();
            _loggerMock = new Mock<ILogger<MealController>>();
            _mapperMock = new Mock<IMapper>();
            _mealController = new MealController(_loggerMock.Object, _mealServiceMock.Object, _mapperMock.Object,
                _jwtTokenHelperMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WhenMealsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetAllMealsAsync(userId)).ReturnsAsync(new List<MealDTO> { new MealDTO() });

            // Act
            var result = await _mealController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<List<MealDTO>>(okResult.Value);
        }

        [Fact]
        public async Task GetAll_ReturnsNoContent_WhenNoMealsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetAllMealsAsync(userId)).ReturnsAsync(new List<MealDTO>());

            // Act
            var result = await _mealController.GetAll();

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _mealController.GetAll();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllWithDetails_ReturnsOkResult_WhenMealsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetAllMealsWithDetailsAsync(userId))
                .ReturnsAsync(new List<MealWithDetailsDTO> { new MealWithDetailsDTO() });

            // Act
            var result = await _mealController.GetAllWithDetails();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<List<MealWithDetailsDTO>>(okResult.Value);
        }

        [Fact]
        public async Task GetAllWithDetails_ReturnsNoContent_WhenNoMealsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetAllMealsWithDetailsAsync(userId))
                .ReturnsAsync(new List<MealWithDetailsDTO>());

            // Act
            var result = await _mealController.GetAllWithDetails();

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetAllWithDetails_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _mealController.GetAllWithDetails();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenMealExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealId, userId)).ReturnsAsync(new MealDTO());

            // Act
            var result = await _mealController.GetById(mealId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<MealDTO>(okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMealDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealId, userId)).ReturnsAsync((MealDTO)null);

            // Act
            var result = await _mealController.GetById(mealId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _mealController.GetById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetByIdWithDetails_ReturnsOkResult_WhenMealExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdWithDetailsAsync(mealId, userId))
                .ReturnsAsync(new MealWithDetailsDTO());

            // Act
            var result = await _mealController.GetByIdWithDetails(mealId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<MealWithDetailsDTO>(okResult.Value);
        }

        [Fact]
        public async Task GetByIdWithDetails_ReturnsNotFound_WhenMealDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdWithDetailsAsync(mealId, userId))
                .ReturnsAsync((MealWithDetailsDTO)null);

            // Act
            var result = await _mealController.GetByIdWithDetails(mealId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetByIdWithDetails_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _mealController.GetByIdWithDetails(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateMeal_ReturnsOkResult_WhenMealIsCreated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealId = Guid.NewGuid();
            var mealCreateDto = new MealCreateDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.CreateMealAsync(mealCreateDto, userId)).ReturnsAsync(mealId);

            // Act
            var result = await _mealController.CreateMeal(mealCreateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(mealId, okResult.Value);
        }

        [Fact]
        public async Task CreateMeal_ReturnsBadRequest_WhenMealDtoIsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);

            // Act
            var result = await _mealController.CreateMeal(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateMeal_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _mealController.CreateMeal(new MealCreateDTO());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateMeal_ReturnsOkResult_WhenMealIsUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealUpdateDto = new MealUpdateDTO { Id = Guid.NewGuid() };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealUpdateDto.Id, userId)).ReturnsAsync(new MealDTO());

            // Act
            var result = await _mealController.UpdateMeal(mealUpdateDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateMeal_ReturnsNotFound_WhenMealDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealUpdateDto = new MealUpdateDTO { Id = Guid.NewGuid() };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealUpdateDto.Id, userId)).ReturnsAsync((MealDTO)null);

            // Act
            var result = await _mealController.UpdateMeal(mealUpdateDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateMeal_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _mealController.UpdateMeal(new MealUpdateDTO());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteMeal_ReturnsOkResult_WhenMealIsDeleted()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealId, userId)).ReturnsAsync(new MealDTO());

            // Act
            var result = await _mealController.DeleteMeal(mealId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteMeal_ReturnsNotFound_WhenMealDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealId, userId)).ReturnsAsync((MealDTO)null);

            // Act
            var result = await _mealController.DeleteMeal(mealId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteMeal_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _mealController.DeleteMeal(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PrepareMeal_ReturnsOkResult_WhenMealIsPrepared()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealPrepareDto = new MealPrepareDTO { Id = Guid.NewGuid() };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealPrepareDto.Id, userId)).ReturnsAsync(new MealDTO());

            // Act
            var result = await _mealController.PrepareMeal(mealPrepareDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PrepareMeal_ReturnsNotFound_WhenMealDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealPrepareDto = new MealPrepareDTO { Id = Guid.NewGuid() };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealPrepareDto.Id, userId)).ReturnsAsync((MealDTO)null);

            // Act
            var result = await _mealController.PrepareMeal(mealPrepareDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PrepareMeal_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _mealController.PrepareMeal(new MealPrepareDTO());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task EatMeal_ReturnsOkResult_WhenMealIsEaten()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealId, userId)).ReturnsAsync(new MealDTO());

            // Act
            var result = await _mealController.EatMeal(mealId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task EatMeal_ReturnsNotFound_WhenMealDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mealId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealId, userId)).ReturnsAsync((MealDTO)null);

            // Act
            var result = await _mealController.EatMeal(mealId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EatMeal_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _mealController.EatMeal(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task EatSnack_ReturnsOkResult_WhenSnackIsEaten()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var snackDto = new SnackCreateDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);

            // Act
            var result = await _mealController.EatSnack(snackDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task EatSnack_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _mealController.EatSnack(new SnackCreateDTO());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetMealTimes_ReturnsOkResult_WithMealTimesDictionary()
        {
            // Act
            var result = await _mealController.GetMealTimes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<Dictionary<string, int>>(okResult.Value);
        }
    }
}