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
using JetBrains.Annotations;
using Xunit;

namespace CebuFitApi.UnitTests.Controllers;

[TestSubject(typeof(StorageItemController))]
public class StorageItemControllerTest
{
    private readonly Mock<ILogger<StorageItemController>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IStorageItemService> _storageItemServiceMock;
    private readonly Mock<IJwtTokenHelper> _jwtTokenHelperMock;
    private readonly StorageItemController _controller;

    public StorageItemControllerTest()
    {
        _loggerMock = new Mock<ILogger<StorageItemController>>();
        _mapperMock = new Mock<IMapper>();
        _storageItemServiceMock = new Mock<IStorageItemService>();
        _jwtTokenHelperMock = new Mock<IJwtTokenHelper>();
        _controller = new StorageItemController(
            _loggerMock.Object,
            _mapperMock.Object,
            _storageItemServiceMock.Object,
            _jwtTokenHelperMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfStorageItems()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetAllStorageItemsAsync(userId))
            .ReturnsAsync(new List<StorageItemDTO> { new StorageItemDTO() });

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<StorageItemDTO>>(okResult.Value);
    }

    [Fact]
    public async Task GetAll_ReturnsNoContent_WhenNoStorageItemsFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetAllStorageItemsAsync(userId))
            .ReturnsAsync(new List<StorageItemDTO>());

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_ReturnsNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task GetAllWithProduct_ReturnsOkResult_WithListOfStorageItemsWithProduct(bool withoutEaten)
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetAllStorageItemsWithProductAsync(userId, withoutEaten))
            .ReturnsAsync(new List<StorageItemWithProductDTO> { new StorageItemWithProductDTO() });

        // Act
        var result = await _controller.GetAllWithProduct(withoutEaten);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<StorageItemWithProductDTO>>(okResult.Value);
    }

    [Fact]
    public async Task GetAllWithProduct_ReturnsNoContent_WhenNoStorageItemsFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetAllStorageItemsWithProductAsync(userId, false))
            .ReturnsAsync(new List<StorageItemWithProductDTO>());

        // Act
        var result = await _controller.GetAllWithProduct();

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task GetAllWithProduct_ReturnsNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetAllWithProduct();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task GetAllByProductIdWithProduct_ReturnsOkResult_WithListOfStorageItems()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetAllStorageItemsByProductIdWithProductAsync(productId, userId))
            .ReturnsAsync(new List<StorageItemWithProductDTO> { new StorageItemWithProductDTO() });

        // Act
        var result = await _controller.GetAllByProductIdWithProduct(productId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<StorageItemWithProductDTO>>(okResult.Value);
    }

    [Fact]
    public async Task GetAllByProductIdWithProduct_ReturnsNoContent_WhenNoStorageItemsFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetAllStorageItemsByProductIdWithProductAsync(productId, userId))
            .ReturnsAsync(new List<StorageItemWithProductDTO>());

        // Act
        var result = await _controller.GetAllByProductIdWithProduct(productId);

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task GetAllByProductIdWithProduct_ReturnsNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetAllByProductIdWithProduct(productId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithStorageItem()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var siId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetStorageItemByIdAsync(siId, userId))
            .ReturnsAsync(new StorageItemDTO());

        // Act
        var result = await _controller.GetById(siId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<StorageItemDTO>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenStorageItemNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var siId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetStorageItemByIdAsync(siId, userId))
            .ReturnsAsync((StorageItemDTO)null);

        // Act
        var result = await _controller.GetById(siId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        var siId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetById(siId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task GetByIdWithProduct_ReturnsOkResult_WithStorageItemWithProduct()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var siId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetStorageItemByIdWithProductAsync(siId, userId))
            .ReturnsAsync(new StorageItemWithProductDTO());

        // Act
        var result = await _controller.GetByIdWithProduct(siId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<StorageItemWithProductDTO>(okResult.Value);
    }

    [Fact]
    public async Task GetByIdWithProduct_ReturnsNotFound_WhenStorageItemNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var siId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetStorageItemByIdWithProductAsync(siId, userId))
            .ReturnsAsync((StorageItemWithProductDTO)null);

        // Act
        var result = await _controller.GetByIdWithProduct(siId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetByIdWithProduct_ReturnsNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        var siId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetByIdWithProduct(siId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task CreateStorageItem_ReturnsOk_WhenStorageItemIsCreated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var storageItemDTO = new StorageItemCreateDTO();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);

        // Act
        var result = await _controller.CreateStorageItem(storageItemDTO);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task CreateStorageItem_ReturnsBadRequest_WhenStorageItemDTOIsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);

        // Act
        var result = await _controller.CreateStorageItem(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Storage item data is null.", badRequestResult.Value);
    }

    [Fact]
    public async Task CreateStorageItem_ReturnsNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        var storageItemDTO = new StorageItemCreateDTO();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.CreateStorageItem(storageItemDTO);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task UpdateStorageItem_ReturnsOk_WhenStorageItemIsUpdated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var storageItemDTO = new StorageItemDTO { Id = Guid.NewGuid() };
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetStorageItemByIdAsync(storageItemDTO.Id, userId))
            .ReturnsAsync(new StorageItemDTO());

        // Act
        var result = await _controller.UpdateStorageItem(storageItemDTO);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateStorageItem_ReturnsNotFound_WhenStorageItemNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var storageItemDTO = new StorageItemDTO { Id = Guid.NewGuid() };
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetStorageItemByIdAsync(storageItemDTO.Id, userId))
            .ReturnsAsync((StorageItemDTO)null);

        // Act
        var result = await _controller.UpdateStorageItem(storageItemDTO);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateStorageItem_ReturnsNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        var storageItemDTO = new StorageItemDTO { Id = Guid.NewGuid() };
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.UpdateStorageItem(storageItemDTO);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("User not found", notFoundResult.Value);
    }

    [Fact]
    public async Task DeleteStorageItem_ReturnsOk_WhenStorageItemIsDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var storageItemId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetStorageItemByIdAsync(storageItemId, userId))
            .ReturnsAsync(new StorageItemDTO());

        // Act
        var result = await _controller.DeleteStorageItem(storageItemId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteStorageItem_ReturnsNotFound_WhenStorageItemNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var storageItemId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _storageItemServiceMock.Setup(x => x.GetStorageItemByIdAsync(storageItemId, userId))
            .ReturnsAsync((StorageItemDTO)null);

        // Act
        var result = await _controller.DeleteStorageItem(storageItemId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteStorageItem_ReturnsNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        var storageItemId = Guid.NewGuid();
        _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.DeleteStorageItem(storageItemId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("User not found", notFoundResult.Value);
    }
}