using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CebuFitApi.Controllers;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CebuFitApi.UnitTests.Controllers
{
    [TestSubject(typeof(ProductController))]
    public class ProductControllerTest
    {
        private readonly Mock<ILogger<ProductController>> _loggerMock;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IJwtTokenHelper> _jwtTokenHelperMock;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _loggerMock = new Mock<ILogger<ProductController>>();
            _productServiceMock = new Mock<IProductService>();
            _mapperMock = new Mock<IMapper>();
            _jwtTokenHelperMock = new Mock<IJwtTokenHelper>();
            _controller = new ProductController(_loggerMock.Object, _productServiceMock.Object, _mapperMock.Object,
                _jwtTokenHelperMock.Object);
        }

        [Theory]
        [InlineData(DataType.Both)]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        public async Task GetAll_ReturnsOkResult_WhenProductsExist(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var products = new List<ProductDTO> { new ProductDTO() };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetAllProductsAsync(userId, dataType)).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll(dataType);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(products, okResult.Value);
        }

        [Theory]
        [InlineData(DataType.Both)]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        public async Task GetAll_ReturnsNoContent_WhenNoProductsExist(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetAllProductsAsync(userId, dataType))
                .ReturnsAsync(new List<ProductDTO>());

            // Act
            var result = await _controller.GetAll(dataType);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Theory]
        [InlineData(DataType.Both)]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        public async Task GetAllWithMacro_ReturnsOkResult_WhenProductsExist(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var products = new List<ProductWithMacroDTO> { new ProductWithMacroDTO() };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetAllProductsWithMacroAsync(userId, dataType)).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllWithMacro(dataType);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(products, okResult.Value);
        }

        [Theory]
        [InlineData(DataType.Both)]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        public async Task GetAllWithMacro_ReturnsNoContent_WhenNoProductsExist(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetAllProductsWithMacroAsync(userId, dataType))
                .ReturnsAsync(new List<ProductWithMacroDTO>());

            // Act
            var result = await _controller.GetAllWithMacro(dataType);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetAllWithMacro_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetAllWithMacro();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Theory]
        [InlineData(DataType.Both)]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        public async Task GetAllWithCategory_ReturnsOkResult_WhenProductsExist(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var products = new List<ProductWithCategoryDTO> { new ProductWithCategoryDTO() };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetAllProductsWithCategoryAsync(userId, dataType)).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllWithCategory(dataType);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(products, okResult.Value);
        }

        [Theory]
        [InlineData(DataType.Both)]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        public async Task GetAllWithCategory_ReturnsNoContent_WhenNoProductsExist(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetAllProductsWithCategoryAsync(userId, dataType))
                .ReturnsAsync(new List<ProductWithCategoryDTO>());

            // Act
            var result = await _controller.GetAllWithCategory(dataType);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetAllWithCategory_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetAllWithCategory();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Theory]
        [InlineData(DataType.Both)]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        public async Task GetAllWithDetails_ReturnsOkResult_WhenProductsExist(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var products = new List<ProductWithDetailsDTO> { new ProductWithDetailsDTO() };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetAllProductsWithDetailsAsync(userId, dataType)).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllWithDetails(dataType);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(products, okResult.Value);
        }

        [Theory]
        [InlineData(DataType.Both)]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        public async Task GetAllWithDetails_ReturnsNoContent_WhenNoProductsExist(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetAllProductsWithDetailsAsync(userId, dataType))
                .ReturnsAsync(new List<ProductWithDetailsDTO>());

            // Act
            var result = await _controller.GetAllWithDetails(dataType);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetAllWithDetails_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetAllWithDetails();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new ProductDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdAsync(productId, userId)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdAsync(productId, userId)).ReturnsAsync((ProductDTO)null);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetByIdWithMacro_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new ProductWithMacroDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdWithMacroAsync(productId, userId)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetByIdWithMacro(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public async Task GetByIdWithMacro_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdWithMacroAsync(productId, userId))
                .ReturnsAsync((ProductWithMacroDTO)null);

            // Act
            var result = await _controller.GetByIdWithMacro(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetByIdWithMacro_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetByIdWithMacro(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetByIdWithCategory_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new ProductWithCategoryDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdWithCategoryAsync(productId, userId)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetByIdWithCategory(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public async Task GetByIdWithCategory_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdWithCategoryAsync(productId, userId))
                .ReturnsAsync((ProductWithCategoryDTO)null);

            // Act
            var result = await _controller.GetByIdWithCategory(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetByIdWithCategory_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetByIdWithCategory(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetByIdWithDetails_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new ProductWithDetailsDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdWithDetailsAsync(productId, userId)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetByIdWithDetails(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public async Task GetByIdWithDetails_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdWithDetailsAsync(productId, userId))
                .ReturnsAsync((ProductWithDetailsDTO)null);

            // Act
            var result = await _controller.GetByIdWithDetails(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetByIdWithDetails_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.GetByIdWithDetails(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateProduct_ReturnsOkResult_WhenProductIsCreated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productDTO = new ProductCreateDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);

            // Act
            var result = await _controller.CreateProduct(productDTO);

            // Assert
            Assert.IsType<OkResult>(result);
            _productServiceMock.Verify(x => x.CreateProductAsync(productDTO, userId), Times.Once);
        }

        [Fact]
        public async Task CreateProduct_ReturnsBadRequest_WhenProductDTOIsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);

            // Act
            var result = await _controller.CreateProduct(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Product data is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateProduct_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.CreateProduct(new ProductCreateDTO());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsOkResult_WhenProductIsUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productDTO = new ProductUpdateDTO { Id = Guid.NewGuid() };
            var existingProduct = new ProductDTO();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdAsync(productDTO.Id, userId)).ReturnsAsync(existingProduct);

            // Act
            var result = await _controller.UpdateProduct(productDTO);

            // Assert
            Assert.IsType<OkResult>(result);
            _productServiceMock.Verify(x => x.UpdateProductAsync(productDTO, userId), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productDTO = new ProductUpdateDTO { Id = Guid.NewGuid() };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdAsync(productDTO.Id, userId)).ReturnsAsync((ProductDTO)null);

            // Act
            var result = await _controller.UpdateProduct(productDTO);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.UpdateProduct(new ProductUpdateDTO());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOkResult_WhenProductIsDeleted()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var existingProduct = new ProductDTO { IsPublic = false };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _jwtTokenHelperMock.Setup(x => x.GetUserRole()).Returns(RoleEnum.User);
            _productServiceMock.Setup(x => x.GetProductByIdAsync(productId, userId)).ReturnsAsync(existingProduct);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<OkResult>(result);
            _productServiceMock.Verify(x => x.DeleteProductAsync(productId), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsBadRequest_WhenProductIsPublic()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var existingProduct = new ProductDTO { IsPublic = true };
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdAsync(productId, userId)).ReturnsAsync(existingProduct);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Cannot delete public product.", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(userId);
            _productServiceMock.Setup(x => x.GetProductByIdAsync(productId, userId)).ReturnsAsync((ProductDTO)null);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            // Act
            var result = await _controller.DeleteProduct(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOkResult_WhenUserIsAdmin()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _jwtTokenHelperMock.Setup(x => x.GetUserRole()).Returns(RoleEnum.Admin);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<OkResult>(result);
            _productServiceMock.Verify(x => x.DeleteProductAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetImportances_ReturnsOkResult_WithImportanceDictionary()
        {
            // Act
            var result = await _controller.GetImportances();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var importanceDict = Assert.IsType<Dictionary<string, int>>(okResult.Value);
            Assert.Equal(Enum.GetValues(typeof(ImportanceEnum)).Length, importanceDict.Count);
        }
    }
}