using AutoMapper;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Services;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace CebuFitApi.UnitTests.Services;

[TestSubject(typeof(UserDemandService))]
public class UserDemandServiceTest
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUserDemandRepository> _demandRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserDemandService _userDemandService;

    public UserDemandServiceTest()
    {
        _mapperMock = new Mock<IMapper>();
        _demandRepositoryMock = new Mock<IUserDemandRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _userDemandService =
            new UserDemandService(_mapperMock.Object, _demandRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("11111111-1111-1111-1111-111111111111")]
    public async Task GetDemandAsync_ShouldReturnDemandDTO_WhenDemandExists(Guid userId)
    {
        // Arrange
        var demandEntity = new UserDemand { UserId = userId };
        var demandDTO = new UserDemandDTO();
        _demandRepositoryMock.Setup(repo => repo.GetDemandAsync(userId)).ReturnsAsync(demandEntity);
        _mapperMock.Setup(mapper => mapper.Map<UserDemandDTO>(demandEntity)).Returns(demandDTO);

        // Act
        var result = await _userDemandService.GetDemandAsync(userId);

        // Assert
        Assert.Equal(demandDTO, result);
    }

    [Fact]
    public async Task GetDemandAsync_ShouldReturnNull_WhenDemandDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _demandRepositoryMock.Setup(repo => repo.GetDemandAsync(userId)).ReturnsAsync((UserDemand)null);

        // Act
        var result = await _userDemandService.GetDemandAsync(userId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateDemandAsync_ShouldUpdateDemand_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var demandUpdateDTO = new UserDemandUpdateDTO();
        var demand = new UserDemand();
        _mapperMock.Setup(mapper => mapper.Map<UserDemand>(demandUpdateDTO)).Returns(demand);
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(new User());

        // Act
        await _userDemandService.UpdateDemandAsync(demandUpdateDTO, userId);

        // Assert
        _demandRepositoryMock.Verify(repo => repo.UpdateDemandAsync(demand, userId), Times.Once);
    }

    [Fact]
    public async Task UpdateDemandAsync_ShouldNotUpdateDemand_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var demandUpdateDTO = new UserDemandUpdateDTO();
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

        // Act
        await _userDemandService.UpdateDemandAsync(demandUpdateDTO, userId);

        // Assert
        _demandRepositoryMock.Verify(repo => repo.UpdateDemandAsync(It.IsAny<UserDemand>(), userId), Times.Never);
    }

    [Fact]
    public async Task AutoCalculateDemandAsync_ShouldUpdateDemand_WhenUserAndDemandExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId };
        var demand = new UserDemand { Id = Guid.NewGuid(), UserId = userId };
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
        _demandRepositoryMock.Setup(repo => repo.GetDemandAsync(userId)).ReturnsAsync(demand);
        _demandRepositoryMock.Setup(repo => repo.UpdateDemandAsync(It.IsAny<UserDemand>(), userId))
            .Returns(Task.CompletedTask);

        // Act
        await _userDemandService.AutoCalculateDemandAsync(userId);

        // Assert
        _demandRepositoryMock.Verify(repo => repo.UpdateDemandAsync(It.IsAny<UserDemand>(), userId), Times.Once);
    }

    [Fact]
    public async Task AutoCalculateDemandAsync_ShouldNotUpdateDemand_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

        // Act
        await _userDemandService.AutoCalculateDemandAsync(userId);

        // Assert
        _demandRepositoryMock.Verify(repo => repo.UpdateDemandAsync(It.IsAny<UserDemand>(), userId), Times.Never);
    }

    [Fact]
    public async Task AutoCalculateDemandAsync_ShouldNotUpdateDemand_WhenDemandDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId };
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
        _demandRepositoryMock.Setup(repo => repo.GetDemandAsync(userId)).ReturnsAsync((UserDemand)null);

        // Act
        await _userDemandService.AutoCalculateDemandAsync(userId);

        // Assert
        _demandRepositoryMock.Verify(repo => repo.UpdateDemandAsync(It.IsAny<UserDemand>(), userId), Times.Never);
    }
}