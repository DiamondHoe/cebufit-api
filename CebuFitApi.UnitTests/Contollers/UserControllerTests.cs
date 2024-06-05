using CebuFitApi.Controllers;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using NUnit.Framework.Legacy;

namespace CebuFitApi.UnitTests.Contollers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<IJwtTokenHelper> _jwtTokenHelperMock;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _jwtTokenHelperMock = new Mock<IJwtTokenHelper>();
            _userController = new UserController(_userServiceMock.Object, _jwtTokenHelperMock.Object);
        }

        [Test]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var userLoginDto = new UserLoginDTO { Login = "testuser", Password = "testpassword" };
            var loggedUser = new User { Id = Guid.NewGuid(), Name = "Test User" };

            _userServiceMock.Setup(x => x.AuthenticateAsync(It.Is<UserLoginDTO>(u => u.Login == "testuser" && u.Password == "testpassword")))
                            .ReturnsAsync(loggedUser);

            _jwtTokenHelperMock.Setup(x => x.GenerateJwtToken(loggedUser.Id, loggedUser.Name, true))
                               .ReturnsAsync("test_token");

            // Act
            var result = await _userController.Login(userLoginDto);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            ClassicAssert.NotNull(okObjectResult);
            Assert.That(okObjectResult.StatusCode, Is.EqualTo(200));
            var token = okObjectResult.Value;
            Assert.That(token, Is.Not.Null);
            Assert.That(Is.EqualTo("{ Token = \"test_token\" }"), Is.EqualTo(token));
        }

        [Test]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var userLoginDto = new UserLoginDTO { Login = "invaliduser", Password = "invalidpassword" };

            _userServiceMock.Setup(x => x.AuthenticateAsync(It.Is<UserLoginDTO>(u => u.Login == "invaliduser" && u.Password == "invalidpassword")))
                            .ReturnsAsync((User)null);

            // Act
            var result = await _userController.Login(userLoginDto);

            // Assert
            ClassicAssert.IsInstanceOf<UnauthorizedResult>(result);
        }
    }
}