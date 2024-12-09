using CebuFitApi.Services;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;

namespace CebuFitApi.UnitTests.Services;

[TestSubject(typeof(ProductTypeService))]
public class ProductTypeServiceTest
{
    private readonly Mock<IProductTypeRepository> _productTypeRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ProductTypeService _productTypeService;

    public ProductTypeServiceTest()
    {
        _productTypeRepositoryMock = new Mock<IProductTypeRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _productTypeService = new ProductTypeService(
            _productTypeRepositoryMock.Object,
            _userRepositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllProductTypesAsync_ReturnsProductTypeDtos()
    {
        // Arrange
        var userIdClaim = Guid.NewGuid();
        var dataType = DataType.Both;
        var productTypes = new List<ProductType> { new ProductType() };
        var productTypeDtos = new List<ProductTypeDto> { new ProductTypeDto() };

        _productTypeRepositoryMock.Setup(repo => repo.GetAllAsync(userIdClaim, dataType))
            .ReturnsAsync(productTypes);
        _mapperMock.Setup(mapper => mapper.Map<List<ProductTypeDto>>(productTypes))
            .Returns(productTypeDtos);

        // Act
        var result = await _productTypeService.GetAllProductTypesAsync(userIdClaim, dataType);

        // Assert
        Assert.Equal(productTypeDtos, result);
    }

    [Fact]
    public async Task GetProductTypeByIdAsync_ReturnsProductTypeDto()
    {
        // Arrange
        var productTypeId = Guid.NewGuid();
        var userIdClaim = Guid.NewGuid();
        var productType = new ProductType();
        var productTypeDto = new ProductTypeDto();

        _productTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(productTypeId, userIdClaim))
            .ReturnsAsync(productType);
        _mapperMock.Setup(mapper => mapper.Map<ProductTypeDto>(productType))
            .Returns(productTypeDto);

        // Act
        var result = await _productTypeService.GetProductTypeByIdAsync(productTypeId, userIdClaim);

        // Assert
        Assert.Equal(productTypeDto, result);
    }

    [Fact]
    public async Task CreateProductTypeAsync_AddsProductType()
    {
        // Arrange
        var productTypeCreateDto = new ProductTypeCreateDto { Type = "NewType" };
        var userIdClaim = Guid.NewGuid();
        var productType = new ProductType { Id = Guid.NewGuid() };
        var user = new User();

        _mapperMock.Setup(mapper => mapper.Map<ProductType>(productTypeCreateDto))
            .Returns(productType);
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userIdClaim))
            .ReturnsAsync(user);

        // Act
        await _productTypeService.CreateProductTypeAsync(productTypeCreateDto, userIdClaim);

        // Assert
        _productTypeRepositoryMock.Verify(repo => repo.AddAsync(productType), Times.Once);
    }

    [Fact]
    public async Task UpdateProductTypeAsync_UpdatesProductType()
    {
        // Arrange
        var productTypeDto = new ProductTypeDto { Id = Guid.NewGuid(), Type = "UpdatedType" };
        var userIdClaim = Guid.NewGuid();
        var productType = new ProductType();
        var user = new User();

        _mapperMock.Setup(mapper => mapper.Map<ProductType>(productTypeDto))
            .Returns(productType);
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userIdClaim))
            .ReturnsAsync(user);

        // Act
        await _productTypeService.UpdateProductTypeAsync(productTypeDto, userIdClaim);

        // Assert
        _productTypeRepositoryMock.Verify(repo => repo.UpdateAsync(productType, userIdClaim), Times.Once);
    }

    [Fact]
    public async Task DeleteProductTypeAsync_DeletesProductType()
    {
        // Arrange
        var productTypeId = Guid.NewGuid();
        var userIdClaim = Guid.NewGuid();

        // Act
        await _productTypeService.DeleteProductTypeAsync(productTypeId, userIdClaim);

        // Assert
        _productTypeRepositoryMock.Verify(repo => repo.DeleteAsync(productTypeId, userIdClaim), Times.Once);
    }
}