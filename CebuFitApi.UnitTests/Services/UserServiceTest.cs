using CebuFitApi.Services;
using CebuFitApi.DTOs;
using CebuFitApi.DTOs.User;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;

namespace CebuFitApi.UnitTests.Services
{
    [TestSubject(typeof(UserService))]
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IStorageItemService> _storageItemServiceMock;
        private readonly Mock<IDayService> _dayServiceMock;
        private readonly Mock<IMealService> _mealServiceMock;
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly Mock<IUserDemandService> _demandServiceMock;
        private readonly Mock<IUserDemandRepository> _demandRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _storageItemServiceMock = new Mock<IStorageItemService>();
            _dayServiceMock = new Mock<IDayService>();
            _mealServiceMock = new Mock<IMealService>();
            _categoryServiceMock = new Mock<ICategoryService>();
            _demandServiceMock = new Mock<IUserDemandService>();
            _demandRepositoryMock = new Mock<IUserDemandRepository>();
            _mapperMock = new Mock<IMapper>();

            _userService = new UserService(
                _userRepositoryMock.Object,
                _storageItemServiceMock.Object,
                _dayServiceMock.Object,
                _mealServiceMock.Object,
                _categoryServiceMock.Object,
                _demandServiceMock.Object,
                _demandRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task AuthenticateAsync_ValidUser_ReturnsAuthenticatedUser()
        {
            // Arrange
            var userLoginDto = new UserLoginDTO();
            var user = new User();
            _mapperMock.Setup(m => m.Map<User>(userLoginDto)).Returns(user);
            _userRepositoryMock.Setup(r => r.AuthenticateAsync(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.AuthenticateAsync(userLoginDto);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(r => r.AuthenticateAsync(user), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ValidUser_ReturnsTrueAndUser()
        {
            // Arrange
            var userCreateDto = new UserCreateDTO();
            var user = new User();
            _mapperMock.Setup(m => m.Map<User>(userCreateDto)).Returns(user);
            _userRepositoryMock.Setup(r => r.CreateAsync(user)).ReturnsAsync(true);

            // Act
            var (isRegistered, createdUser) = await _userService.CreateAsync(userCreateDto);

            // Assert
            Assert.True(isRegistered);
            Assert.NotNull(createdUser);
            _userRepositoryMock.Verify(r => r.CreateAsync(user), Times.Once);
        }

        [Fact]
        public async Task GetDetailsAsync_ValidUserId_ReturnsUserDetails()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User();
            var userDetailsDto = new UserDetailsDTO();
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserDetailsDTO>(user)).Returns(userDetailsDto);

            // Act
            var result = await _userService.GetDetailsAsync(userId);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(r => r.GetByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetByEmailAsync_ValidEmail_ReturnsUserDTO()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User();
            var userDto = new UserDTO();
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserDTO>(user)).Returns(userDto);

            // Act
            var result = await _userService.GetByEmailAsync(email);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(r => r.GetByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task ResetPasswordAsync_ValidEmail_ReturnsNewPassword()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User { Name = "Test User" };
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(user);

            // Act
            var result = await _userService.ResetPasswordAsync(email);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ValidUserId_UpdatesUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userUpdateDto = new UserUpdateDTO();
            var user = new User();
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            await _userService.UpdateAsync(userId, userUpdateDto);

            // Assert
            _userRepositoryMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        //[Fact]
        //public async Task GetSummaryAsync_ValidUserId_ReturnsSummaryDTO()
        //{
        //    // Arrange
        //    var userId = Guid.NewGuid();
        //    var start = DateTime.Now.AddDays(-30);
        //    var end = DateTime.Now;
        //    var summaryDto = new SummaryDTO();
        //    _storageItemServiceMock.Setup(s => s.GetAllStorageItemsWithProductAsync(userId, false))
        //        .ReturnsAsync(new List<StorageItemWithProductDTO>());
        //    _dayServiceMock.Setup(d => d.GetAllDaysWithMealsAsync(userId))
        //        .ReturnsAsync(new List<DayWithMealsDTO>());

        //    // Act
        //    var result = await _userService.GetSummaryAsync(userId, start, end);

        //    // Assert
        //    Assert.NotNull(result);
        //}
    }
}