using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;
using CebuFitApi.Services;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace CebuFitApi.UnitTests.Services
{
    [TestSubject(typeof(DayService))]
    public class DayServiceTest
    {
        private readonly Mock<IDayRepository> _dayRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserDemandService> _userDemandServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly DayService _dayService;

        public DayServiceTest()
        {
            _dayRepositoryMock = new Mock<IDayRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userDemandServiceMock = new Mock<IUserDemandService>();
            _mapperMock = new Mock<IMapper>();
            _dayService = new DayService(_mapperMock.Object, _dayRepositoryMock.Object, _userRepositoryMock.Object,
                _userDemandServiceMock.Object);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("11111111-1111-1111-1111-111111111111")]
        public async Task GetAllDaysAsync_ShouldReturnMappedDays(string userId)
        {
            var userIdGuid = Guid.Parse(userId);
            var days = new List<Day> { new Day { Id = Guid.NewGuid() } };
            var daysDto = new List<DayDTO> { new DayDTO { Id = Guid.NewGuid() } };

            _dayRepositoryMock.Setup(repo => repo.GetAllAsync(userIdGuid)).ReturnsAsync(days);
            _mapperMock.Setup(mapper => mapper.Map<List<DayDTO>>(days)).Returns(daysDto);

            var result = await _dayService.GetAllDaysAsync(userIdGuid);

            Assert.Equal(daysDto, result);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("11111111-1111-1111-1111-111111111111")]
        public async Task GetAllDaysWithMealsAsync_ShouldReturnMappedDaysWithMeals(string userId)
        {
            var userIdGuid = Guid.Parse(userId);
            var days = new List<Day> { new Day { Id = Guid.NewGuid() } };
            var daysDto = new List<DayWithMealsDTO> { new DayWithMealsDTO { Id = Guid.NewGuid() } };
            var userDemand = new UserDemandDTO { Calories = 2000 };

            _dayRepositoryMock.Setup(repo => repo.GetAllWithMealsAsync(userIdGuid)).ReturnsAsync(days);
            _mapperMock.Setup(mapper => mapper.Map<List<DayWithMealsDTO>>(days)).Returns(daysDto);
            _userDemandServiceMock.Setup(service => service.GetDemandAsync(userIdGuid)).ReturnsAsync(userDemand);

            var result = await _dayService.GetAllDaysWithMealsAsync(userIdGuid);

            Assert.Equal(daysDto, result);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "22222222-2222-2222-2222-222222222222")]
        [InlineData("11111111-1111-1111-1111-111111111111", "33333333-3333-3333-3333-333333333333")]
        public async Task GetDayByIdAsync_ShouldReturnMappedDay(string dayId, string userId)
        {
            var dayIdGuid = Guid.Parse(dayId);
            var userIdGuid = Guid.Parse(userId);
            var day = new Day { Id = dayIdGuid };
            var dayDto = new DayDTO { Id = dayIdGuid };

            _dayRepositoryMock.Setup(repo => repo.GetByIdAsync(dayIdGuid, userIdGuid)).ReturnsAsync(day);
            _mapperMock.Setup(mapper => mapper.Map<DayDTO>(day)).Returns(dayDto);

            var result = await _dayService.GetDayByIdAsync(dayIdGuid, userIdGuid);

            Assert.Equal(dayDto, result);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "22222222-2222-2222-2222-222222222222")]
        [InlineData("11111111-1111-1111-1111-111111111111", "33333333-3333-3333-3333-333333333333")]
        public async Task GetDayByIdWithMealsAsync_ShouldReturnMappedDayWithMeals(string dayId, string userId)
        {
            var dayIdGuid = Guid.Parse(dayId);
            var userIdGuid = Guid.Parse(userId);
            var day = new Day { Id = dayIdGuid };
            var dayDto = new DayWithMealsDTO { Id = dayIdGuid };
            var userDemand = new UserDemandDTO { Calories = 2000 };

            _dayRepositoryMock.Setup(repo => repo.GetByIdWithMealsAsync(dayIdGuid, userIdGuid)).ReturnsAsync(day);
            _mapperMock.Setup(mapper => mapper.Map<DayWithMealsDTO>(day)).Returns(dayDto);
            _userDemandServiceMock.Setup(service => service.GetDemandAsync(userIdGuid)).ReturnsAsync(userDemand);

            var result = await _dayService.GetDayByIdWithMealsAsync(dayIdGuid, userIdGuid);

            Assert.Equal(dayDto, result);
        }

        [Theory]
        [InlineData("2023-01-01", "00000000-0000-0000-0000-000000000000")]
        [InlineData("2023-12-31", "11111111-1111-1111-1111-111111111111")]
        public async Task GetDayByDateWithMealsAsync_ShouldReturnMappedDayWithMeals(DateTime date, string userId)
        {
            var userIdGuid = Guid.Parse(userId);
            var day = new Day { Id = Guid.NewGuid() };
            var dayDto = new DayWithMealsDTO { Id = Guid.NewGuid() };
            var userDemand = new UserDemandDTO { Calories = 2000 };

            _dayRepositoryMock.Setup(repo => repo.GetByDateWithMealsAsync(date, userIdGuid)).ReturnsAsync(day);
            _mapperMock.Setup(mapper => mapper.Map<DayWithMealsDTO>(day)).Returns(dayDto);
            _userDemandServiceMock.Setup(service => service.GetDemandAsync(userIdGuid)).ReturnsAsync(userDemand);

            var result = await _dayService.GetDayByDateWithMealsAsync(date, userIdGuid);

            Assert.Equal(dayDto, result);
        }

        [Theory]
        [InlineData("2023-01-01", "00000000-0000-0000-0000-000000000000")]
        [InlineData("2023-12-31", "11111111-1111-1111-1111-111111111111")]
        public async Task CreateDayAsync_ShouldReturnNewDayId(DateTime date, string userId)
        {
            var userIdGuid = Guid.Parse(userId);
            var dayDto = new DayCreateDTO { Date = date };
            var day = new Day { Id = Guid.NewGuid(), Date = date };

            _mapperMock.Setup(mapper => mapper.Map<Day>(dayDto)).Returns(day);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userIdGuid)).ReturnsAsync(new User { Id = userIdGuid });

            var result = await _dayService.CreateDayAsync(dayDto, userIdGuid);

            Assert.Equal(day.Id, result);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "22222222-2222-2222-2222-222222222222")]
        [InlineData("11111111-1111-1111-1111-111111111111", "33333333-3333-3333-3333-333333333333")]
        public async Task UpdateDayAsync_ShouldUpdateDay(string dayId, string userId)
        {
            var dayIdGuid = Guid.Parse(dayId);
            var userIdGuid = Guid.Parse(userId);
            var dayDto = new DayUpdateDTO { Id = dayIdGuid };
            var day = new Day { Id = dayIdGuid };

            _mapperMock.Setup(mapper => mapper.Map<Day>(dayDto)).Returns(day);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userIdGuid)).ReturnsAsync(new User { Id = userIdGuid });

            await _dayService.UpdateDayAsync(dayDto, userIdGuid);

            _dayRepositoryMock.Verify(repo => repo.CreateAsync(day, userIdGuid), Times.Once);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "22222222-2222-2222-2222-222222222222")]
        [InlineData("11111111-1111-1111-1111-111111111111", "33333333-3333-3333-3333-333333333333")]
        public async Task DeleteDayAsync_ShouldDeleteDay(string dayId, string userId)
        {
            var dayIdGuid = Guid.Parse(dayId);
            var userIdGuid = Guid.Parse(userId);

            await _dayService.DeleteDayAsync(dayIdGuid, userIdGuid);

            _dayRepositoryMock.Verify(repo => repo.DeleteAsync(dayIdGuid, userIdGuid), Times.Once);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "44444444-4444-4444-4444-444444444444")]
        [InlineData("11111111-1111-1111-1111-111111111111", "55555555-5555-5555-5555-555555555555")]
        public async Task AddMealToDayAsync_ShouldReturnUpdatedDay(string dayId, string mealId)
        {
            var dayIdGuid = Guid.Parse(dayId);
            var mealIdGuid = Guid.Parse(mealId);
            var day = new Day { Id = dayIdGuid };
            var dayDto = new DayDTO { Id = dayIdGuid };

            _dayRepositoryMock.Setup(repo => repo.AddMealToDayAsync(dayIdGuid, mealIdGuid)).ReturnsAsync(day);
            _mapperMock.Setup(mapper => mapper.Map<DayDTO>(day)).Returns(dayDto);

            var result = await _dayService.AddMealToDayAsync(dayIdGuid, mealIdGuid);

            Assert.Equal(dayDto, result);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "44444444-4444-4444-4444-444444444444")]
        [InlineData("11111111-1111-1111-1111-111111111111", "55555555-5555-5555-5555-555555555555")]
        public async Task RemoveMealFromDayAsync_ShouldReturnUpdatedDay(string dayId, string mealId)
        {
            var dayIdGuid = Guid.Parse(dayId);
            var mealIdGuid = Guid.Parse(mealId);
            var day = new Day { Id = dayIdGuid };
            var dayDto = new DayDTO { Id = dayIdGuid };

            _dayRepositoryMock.Setup(repo => repo.RemoveMealFromDayAsync(dayIdGuid, mealIdGuid)).ReturnsAsync(day);
            _mapperMock.Setup(mapper => mapper.Map<DayDTO>(day)).Returns(dayDto);

            var result = await _dayService.RemoveMealFromDayAsync(dayIdGuid, mealIdGuid);

            Assert.Equal(dayDto, result);
        }

        [Theory]
        [InlineData("2023-01-01", "2023-01-31", "00000000-0000-0000-0000-000000000000")]
        [InlineData("2023-02-01", "2023-02-28", "11111111-1111-1111-1111-111111111111")]
        public async Task GetCostsForDateRangeAsync_ShouldReturnCosts(DateTime start, DateTime end, string userId)
        {
            var userIdGuid = Guid.Parse(userId);
            decimal? expectedCosts = 100m;

            _dayRepositoryMock.Setup(repo => repo.GetCostsForDateRangeAsync(start, end, userIdGuid))
                .ReturnsAsync(expectedCosts);

            var result = await _dayService.GetCostsForDateRangeAsync(start, end, userIdGuid);

            Assert.Equal(expectedCosts, result);
        }

        [Theory]
        [InlineData("2023-01-01", "2023-01-31", "00000000-0000-0000-0000-000000000000")]
        [InlineData("2023-02-01", "2023-02-28", "11111111-1111-1111-1111-111111111111")]
        public async Task GetShoppingForDateRangeAsync_ShouldReturnDays(DateTime start, DateTime end, string userId)
        {
            var userIdGuid = Guid.Parse(userId);
            var days = new List<Day> { new Day { Id = Guid.NewGuid() } };

            _dayRepositoryMock.Setup(repo => repo.GetShoppingListForDateRangeAsync(start, end, userIdGuid))
                .ReturnsAsync(days);

            var result = await _dayService.GetShoppingForDateRangeAsync(start, end, userIdGuid);

            Assert.Equal(days, result);
        }
    }
}