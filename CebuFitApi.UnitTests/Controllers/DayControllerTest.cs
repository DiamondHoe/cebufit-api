using CebuFitApi.Controllers;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CebuFitApi.Models;
using JetBrains.Annotations;
using Xunit;

namespace CebuFitApi.UnitTests.Controllers;

[TestSubject(typeof(DayController))]
public class DayControllerTest
{
    private readonly Mock<ILogger<DayController>> _loggerMock;
    private readonly Mock<IDayService> _dayServiceMock;
    private readonly Mock<IMealService> _mealServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IJwtTokenHelper> _jwtTokenHelperMock;
    private readonly Mock<IExcelHelper> _excelHelperMock;
    private readonly DayController _controller;

    public DayControllerTest()
    {
        _loggerMock = new Mock<ILogger<DayController>>();
        _dayServiceMock = new Mock<IDayService>();
        _mealServiceMock = new Mock<IMealService>();
        _mapperMock = new Mock<IMapper>();
        _jwtTokenHelperMock = new Mock<IJwtTokenHelper>();
        _excelHelperMock = new Mock<IExcelHelper>();

        _controller = new DayController(
            _loggerMock.Object,
            _dayServiceMock.Object,
            _mealServiceMock.Object,
            _mapperMock.Object,
            _jwtTokenHelperMock.Object,
            _excelHelperMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithDays()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetAllDaysAsync(userId)).ReturnsAsync(new List<DayDTO> { new DayDTO() });

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<DayDTO>>(okResult.Value);
    }

    [Fact]
    public async Task GetAll_ReturnsNoContent_WhenNoDays()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetAllDaysAsync(userId)).ReturnsAsync(new List<DayDTO>());

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetAllWithMeals_ReturnsOkResult_WithDays()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetAllDaysWithMealsAsync(userId)).ReturnsAsync(new List<DayWithMealsDTO> { new DayWithMealsDTO() });

        // Act
        var result = await _controller.GetAllWithMeals();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<DayWithMealsDTO>>(okResult.Value);
    }

    [Fact]
    public async Task GetAllWithMeals_ReturnsNoContent_WhenNoDays()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetAllDaysWithMealsAsync(userId)).ReturnsAsync(new List<DayWithMealsDTO>());

        // Act
        var result = await _controller.GetAllWithMeals();

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task GetAllWithMeals_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetAllWithMeals();

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithDay()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdAsync(dayId, userId)).ReturnsAsync(new DayDTO());

        // Act
        var result = await _controller.GetById(dayId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<DayDTO>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenDayNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdAsync(dayId, userId)).ReturnsAsync((DayDTO)null);

        // Act
        var result = await _controller.GetById(dayId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var dayId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetById(dayId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetByIdWithMeals_ReturnsOkResult_WithDay()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdWithMealsAsync(dayId, userId)).ReturnsAsync(new DayWithMealsDTO());

        // Act
        var result = await _controller.GetByIdWithMeals(dayId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<DayWithMealsDTO>(okResult.Value);
    }

    [Fact]
    public async Task GetByIdWithMeals_ReturnsNotFound_WhenDayNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdWithMealsAsync(dayId, userId)).ReturnsAsync((DayWithMealsDTO)null);

        // Act
        var result = await _controller.GetByIdWithMeals(dayId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetByIdWithMeals_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var dayId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetByIdWithMeals(dayId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetByDateWithMeals_ReturnsOkResult_WithDay()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var date = DateTime.UtcNow;
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByDateWithMealsAsync(date, userId)).ReturnsAsync(new DayWithMealsDTO());

        // Act
        var result = await _controller.GetByDateWithMeals(date);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<DayWithMealsDTO>(okResult.Value);
    }

    [Fact]
    public async Task GetByDateWithMeals_ReturnsNotFound_WhenDayNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var date = DateTime.UtcNow;
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByDateWithMealsAsync(date, userId)).ReturnsAsync((DayWithMealsDTO)null);

        // Act
        var result = await _controller.GetByDateWithMeals(date);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetByDateWithMeals_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var date = DateTime.UtcNow;
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetByDateWithMeals(date);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateDay_ReturnsOkResult_WithDayId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayDTO = new DayCreateDTO { Date = DateTime.UtcNow };
        var dayId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.CreateDayAsync(dayDTO, userId)).ReturnsAsync(dayId);

        // Act
        var result = await _controller.CreateDay(dayDTO);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(dayId, okResult.Value);
    }

    [Fact]
    public async Task CreateDay_ReturnsBadRequest_WhenDayDTOIsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);

        // Act
        var result = await _controller.CreateDay(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateDay_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var dayDTO = new DayCreateDTO { Date = DateTime.UtcNow };
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.CreateDay(dayDTO);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateDay_ReturnsOkResult_WhenDayUpdated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayDTO = new DayUpdateDTO { Id = Guid.NewGuid() };
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdAsync(dayDTO.Id, userId)).ReturnsAsync(new DayDTO());

        // Act
        var result = await _controller.UpdateDay(dayDTO);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateDay_ReturnsNotFound_WhenDayNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayDTO = new DayUpdateDTO { Id = Guid.NewGuid() };
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdAsync(dayDTO.Id, userId)).ReturnsAsync((DayDTO)null);

        // Act
        var result = await _controller.UpdateDay(dayDTO);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateDay_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var dayDTO = new DayUpdateDTO { Id = Guid.NewGuid() };
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.UpdateDay(dayDTO);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteDay_ReturnsOkResult_WhenDayDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdAsync(dayId, userId)).ReturnsAsync(new DayDTO());

        // Act
        var result = await _controller.DeleteDay(dayId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteDay_ReturnsNotFound_WhenDayNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdAsync(dayId, userId)).ReturnsAsync((DayDTO)null);

        // Act
        var result = await _controller.DeleteDay(dayId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteDay_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var dayId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.DeleteDay(dayId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task AddMealToDay_ReturnsOkResult_WithUpdatedDay()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayId = Guid.NewGuid();
        var mealId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdAsync(dayId, userId)).ReturnsAsync(new DayDTO());
        _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealId, userId)).ReturnsAsync(new MealDTO());
        _dayServiceMock.Setup(x => x.AddMealToDayAsync(dayId, mealId)).ReturnsAsync(new DayDTO());

        // Act
        var result = await _controller.AddMealToDay(dayId, mealId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<DayDTO>(okResult.Value);
    }

    [Fact]
    public async Task AddMealToDay_ReturnsNotFound_WhenDayOrMealNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayId = Guid.NewGuid();
        var mealId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdAsync(dayId, userId)).ReturnsAsync((DayDTO)null);
        _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealId, userId)).ReturnsAsync((MealDTO)null);

        // Act
        var result = await _controller.AddMealToDay(dayId, mealId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task AddMealToDay_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var dayId = Guid.NewGuid();
        var mealId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.AddMealToDay(dayId, mealId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task RemoveMealFromDay_ReturnsOkResult_WhenMealRemoved()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayId = Guid.NewGuid();
        var mealId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdAsync(dayId, userId)).ReturnsAsync(new DayDTO());
        _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealId, userId)).ReturnsAsync(new MealDTO());

        // Act
        var result = await _controller.RemoveMealFromDay(dayId, mealId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task RemoveMealFromDay_ReturnsNotFound_WhenDayOrMealNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dayId = Guid.NewGuid();
        var mealId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetDayByIdAsync(dayId, userId)).ReturnsAsync((DayDTO)null);
        _mealServiceMock.Setup(x => x.GetMealByIdAsync(mealId, userId)).ReturnsAsync((MealDTO)null);

        // Act
        var result = await _controller.RemoveMealFromDay(dayId, mealId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task RemoveMealFromDay_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var dayId = Guid.NewGuid();
        var mealId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.RemoveMealFromDay(dayId, mealId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetCostsForDateRange_ReturnsOkResult_WithCosts()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var start = DateTime.UtcNow.AddDays(-1);
        var end = DateTime.UtcNow;
        var costs = 100m;
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetCostsForDateRangeAsync(start, end, userId)).ReturnsAsync(costs);

        // Act
        var result = await _controller.GetCostsForDateRange(start, end);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(costs, okResult.Value);
    }

    [Fact]
    public async Task GetCostsForDateRange_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var start = DateTime.UtcNow.AddDays(-1);
        var end = DateTime.UtcNow;
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetCostsForDateRange(start, end);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetShoppingList_ReturnsFileResult_WithExcelFile()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var start = DateTime.UtcNow.AddDays(-1);
        var end = DateTime.UtcNow;
        var excelBytes = new byte[] { 1, 2, 3 };
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetShoppingForDateRangeAsync(start, end, userId)).ReturnsAsync(new List<Day>());
        _excelHelperMock.Setup(x => x.GenerateExcel(It.IsAny<List<Day>>())).ReturnsAsync(excelBytes);

        // Act
        var result = await _controller.GetShoppingList(start, end);

        // Assert
        var fileResult = Assert.IsType<FileContentResult>(result);
        Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileResult.ContentType);
        Assert.Equal("shopping_list.xlsx", fileResult.FileDownloadName);
    }

    [Fact]
    public async Task GetShoppingList_ReturnsBadRequest_WhenExcelGenerationFails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var start = DateTime.UtcNow.AddDays(-1);
        var end = DateTime.UtcNow;
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _dayServiceMock.Setup(x => x.GetShoppingForDateRangeAsync(start, end, userId)).ReturnsAsync(new List<Day>());
        _excelHelperMock.Setup(x => x.GenerateExcel(It.IsAny<List<Day>>())).ThrowsAsync(new Exception("Error generating Excel file."));

        // Act
        var result = await _controller.GetShoppingList(start, end);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error generating Excel file.", badRequestResult.Value);
    }

    [Fact]
    public async Task GetShoppingList_ReturnsNotFound_WhenUserNotFound()
    {
        // Arrange
        var start = DateTime.UtcNow.AddDays(-1);
        var end = DateTime.UtcNow;
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetShoppingList(start, end);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}