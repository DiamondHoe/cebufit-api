using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Services;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace CebuFitApi.UnitTests.Services
{
    [TestSubject(typeof(MealService))]
    public class MealServiceTest
    {
        private readonly Mock<IMealRepository> _mealRepositoryMock;
        private readonly Mock<IIngredientService> _ingredientServiceMock;
        private readonly Mock<IIngredientRepository> _ingredientRepositoryMock;
        private readonly Mock<IStorageItemService> _storageItemServiceMock;
        private readonly Mock<IStorageItemRepository> _storageItemRepositoryMock;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IDayService> _dayServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MealService _mealService;

        public MealServiceTest()
        {
            _mealRepositoryMock = new Mock<IMealRepository>();
            _ingredientServiceMock = new Mock<IIngredientService>();
            _ingredientRepositoryMock = new Mock<IIngredientRepository>();
            _storageItemServiceMock = new Mock<IStorageItemService>();
            _storageItemRepositoryMock = new Mock<IStorageItemRepository>();
            _productServiceMock = new Mock<IProductService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _dayServiceMock = new Mock<IDayService>();
            _mapperMock = new Mock<IMapper>();

            _mealService = new MealService(
                _mealRepositoryMock.Object,
                _ingredientServiceMock.Object,
                _ingredientRepositoryMock.Object,
                _storageItemServiceMock.Object,
                _userRepositoryMock.Object,
                _storageItemRepositoryMock.Object,
                _productServiceMock.Object,
                _dayServiceMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllMealsAsync_ShouldReturnListOfMeals()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var meals = new List<Meal> { new Meal { Id = Guid.NewGuid() } };
            var mealDTOs = new List<MealDTO> { new MealDTO { Id = Guid.NewGuid() } };

            _mealRepositoryMock.Setup(repo => repo.GetAllAsync(userId)).ReturnsAsync(meals);
            _mapperMock.Setup(mapper => mapper.Map<List<MealDTO>>(meals)).Returns(mealDTOs);

            // Act
            var result = await _mealService.GetAllMealsAsync(userId);

            // Assert
            Assert.Equal(mealDTOs, result);
        }

        //[Fact]
        //public async Task GetAllMealsWithDetailsAsync_ShouldReturnListOfMealsWithDetails()
        //{
        //    // Arrange
        //    var userId = Guid.NewGuid();
        //    var meals = new List<Meal> { new Meal { Id = Guid.NewGuid() } };
        //    var mealDTOs = new List<MealWithDetailsDTO> { new MealWithDetailsDTO { Id = Guid.NewGuid() } };

        //    _mealRepositoryMock.Setup(repo => repo.GetAllWithDetailsAsync(userId)).ReturnsAsync(meals);
        //    _mapperMock.Setup(mapper => mapper.Map<List<MealWithDetailsDTO>>(meals)).Returns(mealDTOs);

        //    // Act
        //    var result = await _mealService.GetAllMealsWithDetailsAsync(userId);

        //    // Assert
        //    Assert.Equal(mealDTOs, result);
        //}

        [Fact]
        public async Task GetMealByIdAsync_ShouldReturnMeal()
        {
            // Arrange
            var mealId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var meal = new Meal { Id = mealId };
            var mealDTO = new MealDTO { Id = mealId };

            _mealRepositoryMock.Setup(repo => repo.GetByIdAsync(mealId, userId)).ReturnsAsync(meal);
            _mapperMock.Setup(mapper => mapper.Map<MealDTO>(meal)).Returns(mealDTO);

            // Act
            var result = await _mealService.GetMealByIdAsync(mealId, userId);

            // Assert
            Assert.Equal(mealDTO, result);
        }

        //[Fact]
        //public async Task GetMealByIdWithDetailsAsync_ShouldReturnMealWithDetails()
        //{
        //    // Arrange
        //    var mealId = Guid.NewGuid();
        //    var userId = Guid.NewGuid();
        //    var meal = new Meal { Id = mealId };
        //    var mealDTO = new MealWithDetailsDTO { Id = mealId };

        //    _mealRepositoryMock.Setup(repo => repo.GetByIdWithDetailsAsync(mealId, userId)).ReturnsAsync(meal);
        //    _mapperMock.Setup(mapper => mapper.Map<MealWithDetailsDTO>(meal)).Returns(mealDTO);

        //    // Act
        //    var result = await _mealService.GetMealByIdWithDetailsAsync(mealId, userId);

        //    // Assert
        //    Assert.Equal(mealDTO, result);
        //}

        [Fact]
        public async Task CreateMealAsync_ShouldReturnNewMealId()
        {
            // Arrange
            var mealDTO = new MealCreateDTO { Name = "Test Meal" };
            var userId = Guid.NewGuid();
            var meal = new Meal();

            _mapperMock.Setup(mapper => mapper.Map<Meal>(mealDTO)).Returns(meal);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(new User());
            _mealRepositoryMock.Setup(repo => repo.CreateAsync(meal, userId)).ReturnsAsync(meal.Id);

            // Act
            var result = await _mealService.CreateMealAsync(mealDTO, userId);

            // Assert
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task UpdateMealAsync_ShouldUpdateMeal()
        {
            // Arrange
            var mealDTO = new MealUpdateDTO { Id = Guid.NewGuid() };
            var userId = Guid.NewGuid();
            var meal = new Meal { Id = mealDTO.Id };

            _mapperMock.Setup(mapper => mapper.Map<Meal>(mealDTO)).Returns(meal);
            _mealRepositoryMock.Setup(repo => repo.GetByIdAsync(mealDTO.Id, userId)).ReturnsAsync(meal);

            // Act
            await _mealService.UpdateMealAsync(mealDTO, userId);

            // Assert
            _mealRepositoryMock.Verify(repo => repo.UpdateAsync(meal, userId), Times.Once);
        }

        [Fact]
        public async Task DeleteMealAsync_ShouldDeleteMeal()
        {
            // Arrange
            var mealId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(new User());

            // Act
            await _mealService.DeleteMealAsync(mealId, userId);

            // Assert
            _mealRepositoryMock.Verify(repo => repo.DeleteAsync(mealId, userId), Times.Once);
        }

        [Fact]
        public async Task PrepareMealAsync_ShouldPrepareMeal()
        {
            // Arrange
            var mealPrepareDTO = new MealPrepareDTO { Id = Guid.NewGuid() };
            var userId = Guid.NewGuid();
            var meal = new Meal { Id = mealPrepareDTO.Id };

            _mealRepositoryMock.Setup(repo => repo.GetByIdAsync(mealPrepareDTO.Id, userId)).ReturnsAsync(meal);

            // Act
            await _mealService.PrepareMealAsync(mealPrepareDTO, userId);

            // Assert
            _mealRepositoryMock.Verify(repo => repo.UpdateAsync(meal, userId), Times.Once);
        }

        [Fact]
        public async Task EatMealAsync_ShouldMarkMealAsEaten()
        {
            // Arrange
            var mealId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var meal = new Meal { Id = mealId };

            _mealRepositoryMock.Setup(repo => repo.GetByIdAsync(mealId, userId)).ReturnsAsync(meal);

            // Act
            await _mealService.EatMealAsync(mealId, userId);

            // Assert
            _mealRepositoryMock.Verify(repo => repo.UpdateAsync(meal, userId), Times.Once);
        }

        //[Fact]
        //public async Task EatSnackAsync_ShouldCreateAndPrepareSnack()
        //{
        //    // Arrange
        //    var snackCreateDTO = new SnackCreateDTO
        //    {
        //        Name = "Test Snack",
        //        Ingredient = new IngredientCreateDTO(),
        //        StorageItem = new StorageItemPrepareDTO(),
        //        DayId = Guid.NewGuid()
        //    };
        //    var userId = Guid.NewGuid();
        //    var mealId = Guid.NewGuid();

        //    _mealRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Meal>(), userId)).ReturnsAsync(mealId);

        //    // Act
        //    await _mealService.EatSnackAsync(snackCreateDTO, userId);

        //    // Assert
        //    _dayServiceMock.Verify(service => service.AddMealToDayAsync(snackCreateDTO.DayId, mealId), Times.Once);
        //}
    }
}