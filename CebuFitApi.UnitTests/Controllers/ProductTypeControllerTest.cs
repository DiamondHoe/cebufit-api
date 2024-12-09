using CebuFitApi.Controllers;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CebuFitApi.UnitTests.Controllers;

[TestSubject(typeof(ProductTypeController))]
public class ProductTypeControllerTest
{
    private readonly Mock<IProductTypeService> _mockProductTypeService;
    private readonly Mock<IJwtTokenHelper> _mockJwtTokenHelper;
    private readonly ProductTypeController _controller;

    public ProductTypeControllerTest()
    {
        _mockProductTypeService = new Mock<IProductTypeService>();
        _mockJwtTokenHelper = new Mock<IJwtTokenHelper>();
        _controller = new ProductTypeController(_mockProductTypeService.Object, _mockJwtTokenHelper.Object);
    }

    [Theory]
    [InlineData(DataType.Both)]
    [InlineData(DataType.Public)]
    [InlineData(DataType.Private)]
    public async Task GetAll_ShouldReturnOk_WhenProductTypesExist(DataType dataType)
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
        var productTypes = new List<ProductTypeDto> { new ProductTypeDto { Id = Guid.NewGuid(), Type = "Type1" } };
        _mockProductTypeService.Setup(x => x.GetAllProductTypesAsync(userId, dataType)).ReturnsAsync(productTypes);

        // Act
        var result = await _controller.GetAll(dataType);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(productTypes, okResult.Value);
    }

    [Fact]
    public async Task GetAll_ShouldReturnNoContent_WhenNoProductTypesExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _mockProductTypeService.Setup(x => x.GetAllProductTypesAsync(userId, DataType.Both))
            .ReturnsAsync(new List<ProductTypeDto>());

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_ShouldReturnNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenProductTypeExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var productTypeId = Guid.NewGuid();
        var productType = new ProductTypeDto { Id = productTypeId, Type = "Type1" };
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _mockProductTypeService.Setup(x => x.GetProductTypeByIdAsync(productTypeId, userId)).ReturnsAsync(productType);

        // Act
        var result = await _controller.GetById(productTypeId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(productType, okResult.Value);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenProductTypeDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var productTypeId = Guid.NewGuid();
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _mockProductTypeService.Setup(x => x.GetProductTypeByIdAsync(productTypeId, userId))
            .ReturnsAsync((ProductTypeDto)null);

        // Act
        var result = await _controller.GetById(productTypeId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.GetById(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateProductType_ShouldReturnOk_WhenProductTypeIsCreated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var productTypeCreateDto = new ProductTypeCreateDto { Type = "NewType" };
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);

        // Act
        var result = await _controller.CreateProductType(productTypeCreateDto);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task CreateProductType_ShouldReturnBadRequest_WhenProductTypeCreateDtoIsNull()
    {
        // Act
        var result = await _controller.CreateProductType(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateProductType_ShouldReturnNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);
        var productTypeCreateDto = new ProductTypeCreateDto { Type = "NewType" };

        // Act
        var result = await _controller.CreateProductType(productTypeCreateDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task UpdateProductType_ShouldReturnOk_WhenProductTypeIsUpdated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var productTypeDto = new ProductTypeDto { Id = Guid.NewGuid(), Type = "UpdatedType" };
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _mockProductTypeService.Setup(x => x.GetProductTypeByIdAsync(productTypeDto.Id, userId))
            .ReturnsAsync(productTypeDto);

        // Act
        var result = await _controller.UpdateProductType(productTypeDto);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateProductType_ShouldReturnNotFound_WhenProductTypeDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var productTypeDto = new ProductTypeDto { Id = Guid.NewGuid(), Type = "UpdatedType" };
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _mockProductTypeService.Setup(x => x.GetProductTypeByIdAsync(productTypeDto.Id, userId))
            .ReturnsAsync((ProductTypeDto)null);

        // Act
        var result = await _controller.UpdateProductType(productTypeDto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateProductType_ShouldReturnNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);
        var productTypeDto = new ProductTypeDto { Id = Guid.NewGuid(), Type = "UpdatedType" };

        // Act
        var result = await _controller.UpdateProductType(productTypeDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteProductType_ShouldReturnOk_WhenProductTypeIsDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var productTypeId = Guid.NewGuid();
        var productType = new ProductTypeDto { Id = productTypeId, IsPublic = false };
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _mockProductTypeService.Setup(x => x.GetProductTypeByIdAsync(productTypeId, userId)).ReturnsAsync(productType);

        // Act
        var result = await _controller.DeleteProductType(productTypeId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteProductType_ShouldReturnNotFound_WhenProductTypeDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var productTypeId = Guid.NewGuid();
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _mockProductTypeService.Setup(x => x.GetProductTypeByIdAsync(productTypeId, userId))
            .ReturnsAsync((ProductTypeDto)null);

        // Act
        var result = await _controller.DeleteProductType(productTypeId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteProductType_ShouldReturnBadRequest_WhenProductTypeIsPublic()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var productTypeId = Guid.NewGuid();
        var productType = new ProductTypeDto { Id = productTypeId, IsPublic = true };
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(userId);
        _mockProductTypeService.Setup(x => x.GetProductTypeByIdAsync(productTypeId, userId)).ReturnsAsync(productType);

        // Act
        var result = await _controller.DeleteProductType(productTypeId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeleteProductType_ShouldReturnNotFound_WhenUserIdIsEmpty()
    {
        // Arrange
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        // Act
        var result = await _controller.DeleteProductType(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}