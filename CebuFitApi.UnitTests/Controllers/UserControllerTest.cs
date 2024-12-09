using CebuFitApi.Controllers;
using CebuFitApi.DTOs;
using CebuFitApi.DTOs.User;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CebuFitApi.UnitTests.Controllers
{
    [TestSubject(typeof(UserController))]
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IJwtTokenHelper> _mockJwtTokenHelper;
        private readonly Mock<IUserDemandService> _mockDemandService;
        private readonly UserController _controller;

        public UserControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mockJwtTokenHelper = new Mock<IJwtTokenHelper>();
            _mockDemandService = new Mock<IUserDemandService>();
            _controller = new UserController(_mockUserService.Object, _mockJwtTokenHelper.Object,
                _mockDemandService.Object);
        }

        [Theory]
        [InlineData("validUser", "validPassword", true)]
        [InlineData("invalidUser", "invalidPassword", false)]
        public async Task Login_ShouldReturnExpectedResult(string login, string password, bool isAuthenticated)
        {
            // Arrange
            var userLoginDto = new UserLoginDTO { Login = login, Password = password };
            var user = isAuthenticated ? new User() : null;
            _mockUserService.Setup(s => s.AuthenticateAsync(userLoginDto)).ReturnsAsync(user);
            _mockJwtTokenHelper.Setup(h => h.GenerateJwtToken(It.IsAny<User>(), It.IsAny<bool?>()))
                .ReturnsAsync("token");

            // Act
            var result = await _controller.Login(userLoginDto);

            // Assert
            if (isAuthenticated)
            {
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.NotNull(okResult.Value);
            }
            else
            {
                Assert.IsType<UnauthorizedObjectResult>(result);
            }
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenUserIsNull()
        {
            // Act
            var result = await _controller.Register(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Register_ShouldReturnExpectedResult(bool registerSuccess)
        {
            // Arrange
            var userCreateDto = new UserCreateDTO { Login = "newUser", Password = "password" };
            var user = new User();
            _mockUserService.Setup(s => s.CreateAsync(userCreateDto)).ReturnsAsync((registerSuccess, user));
            _mockJwtTokenHelper.Setup(h => h.GenerateJwtToken(It.IsAny<User>(), It.IsAny<bool?>()))
                .ReturnsAsync("token");

            // Act
            var result = await _controller.Register(userCreateDto);

            // Assert
            if (registerSuccess)
            {
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.NotNull(okResult.Value);
            }
            else
            {
                Assert.IsType<ConflictObjectResult>(result);
            }
        }

        [Fact]
        public async Task GetUserDetailsAsync_ShouldReturnNotFound_WhenUserIdIsEmpty()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(h => h.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetUserDetailsAsync();
            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetUserDetailsAsync_ShouldReturnOk_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDetails = new UserDetailsDTO();
            _mockJwtTokenHelper.Setup(h => h.GetCurrentUserId()).Returns(userId);
            _mockUserService.Setup(s => s.GetDetailsAsync(userId)).ReturnsAsync(userDetails);

            // Act
            var result = await _controller.GetUserDetailsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(userDetails, okResult.Value);
        }

        [Fact]
        public async Task GetSummaryAsync_ShouldReturnNotFound_WhenUserIdIsEmpty()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(h => h.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetSummaryAsync(DateTime.Now, DateTime.Now);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetSummaryAsync_ShouldReturnOk_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var summary = new SummaryDTO();
            _mockJwtTokenHelper.Setup(h => h.GetCurrentUserId()).Returns(userId);
            _mockUserService.Setup(s => s.GetSummaryAsync(userId, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(summary);

            // Act
            var result = await _controller.GetSummaryAsync(DateTime.Now, DateTime.Now);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(summary, okResult.Value);
        }

        [Fact]
        public async Task ResetPassword_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserService.Setup(s => s.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((UserDTO)null);

            // Act
            var result = await _controller.ResetPassword("email@example.com");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task ResetPassword_ShouldReturnOk_WhenPasswordResetSuccessful()
        {
            // Arrange
            var email = "email@example.com";
            _mockUserService.Setup(s => s.GetByEmailAsync(email)).ReturnsAsync(new UserDTO());
            _mockUserService.Setup(s => s.ResetPasswordAsync(email)).ReturnsAsync("newPassword");

            // Act
            var result = await _controller.ResetPassword(email);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnNotFound_WhenUserIdIsEmpty()
        {
            // Arrange
            _mockJwtTokenHelper.Setup(h => h.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.UpdateUser(new UserUpdateDTO());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnOk_WhenUpdateSuccessful()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockJwtTokenHelper.Setup(h => h.GetCurrentUserId()).Returns(userId);

            // Act
            var result = await _controller.UpdateUser(new UserUpdateDTO());

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}