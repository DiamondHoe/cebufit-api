using CebuFitApi.Controllers;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.Interfaces;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;

namespace CebuFitApi.UnitTests.Controllers
{
    [TestSubject(typeof(UserDemandController))]
    public class UserDemandControllerTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IJwtTokenHelper> _jwtTokenHelperMock;
        private readonly Mock<IUserDemandService> _demandServiceMock;
        private readonly UserDemandController _controller;

        public UserDemandControllerTest()
        {
            _mapperMock = new Mock<IMapper>();
            _jwtTokenHelperMock = new Mock<IJwtTokenHelper>();
            _demandServiceMock = new Mock<IUserDemandService>();
            _controller = new UserDemandController(_mapperMock.Object, _jwtTokenHelperMock.Object,
                _demandServiceMock.Object);
        }

        [Fact]
        public async Task GetDemand_UserIdNotEmpty_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _demandServiceMock.Setup(x => x.GetDemandAsync(userId)).ReturnsAsync(new UserDemandDTO());

            // Act
            var result = await _controller.GetDemand();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetDemand_UserIdNotEmpty_ReturnsNoContent()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _demandServiceMock.Setup(x => x.GetDemandAsync(userId)).ReturnsAsync((UserDemandDTO)null);

            // Act
            var result = await _controller.GetDemand();

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetDemand_UserIdEmpty_ReturnsNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetDemand();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateDemand_UserIdNotEmpty_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var demandUpdateDTO = new UserDemandUpdateDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _demandServiceMock.Setup(x => x.GetDemandAsync(userId)).ReturnsAsync(new UserDemandDTO());

            // Act
            var result = await _controller.UpdateDemand(demandUpdateDTO);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateDemand_UserIdNotEmpty_ReturnsNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var demandUpdateDTO = new UserDemandUpdateDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _demandServiceMock.Setup(x => x.GetDemandAsync(userId)).ReturnsAsync((UserDemandDTO)null);

            // Act
            var result = await _controller.UpdateDemand(demandUpdateDTO);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateDemand_UserIdEmpty_ReturnsNotFound()
        {
            // Arrange
            var demandUpdateDTO = new UserDemandUpdateDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.UpdateDemand(demandUpdateDTO);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AutoCalculateDemand_UserIdNotEmpty_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);

            // Act
            var result = await _controller.AutoCalculateDemand();

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AutoCalculateDemand_UserIdEmpty_ReturnsNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.AutoCalculateDemand();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}