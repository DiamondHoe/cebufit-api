using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Services;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace CebuFitApi.UnitTests.Services
{
    [TestSubject(typeof(ProductService))]
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IProductTypeRepository> _productTypeRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _productService;

        public ProductServiceTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productTypeRepositoryMock = new Mock<IProductTypeRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _productService = new ProductService(
                _mapperMock.Object,
                _productRepositoryMock.Object,
                _productTypeRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _userRepositoryMock.Object);
        }

        [Theory]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        [InlineData(DataType.Both)]
        public async Task GetAllProductsAsync_ShouldReturnMappedProducts(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var products = new List<Product> { new Product() };
            var productDTOs = new List<ProductDTO> { new ProductDTO() };

            _productRepositoryMock.Setup(repo => repo.GetAllAsync(userId, dataType)).ReturnsAsync(products);
            _mapperMock.Setup(mapper => mapper.Map<List<ProductDTO>>(products)).Returns(productDTOs);

            // Act
            var result = await _productService.GetAllProductsAsync(userId, dataType);

            // Assert
            Assert.Equal(productDTOs, result);
        }

        [Theory]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        [InlineData(DataType.Both)]
        public async Task GetAllProductsWithMacroAsync_ShouldReturnMappedProducts(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var products = new List<Product> { new Product() };
            var productDTOs = new List<ProductWithMacroDTO> { new ProductWithMacroDTO() };

            _productRepositoryMock.Setup(repo => repo.GetAllAsync(userId, dataType)).ReturnsAsync(products);
            _mapperMock.Setup(mapper => mapper.Map<List<ProductWithMacroDTO>>(products)).Returns(productDTOs);

            // Act
            var result = await _productService.GetAllProductsWithMacroAsync(userId, dataType);

            // Assert
            Assert.Equal(productDTOs, result);
        }

        [Theory]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        [InlineData(DataType.Both)]
        public async Task GetAllProductsWithCategoryAsync_ShouldReturnMappedProducts(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var products = new List<Product> { new Product() };
            var productDTOs = new List<ProductWithCategoryDTO> { new ProductWithCategoryDTO() };

            _productRepositoryMock.Setup(repo => repo.GetAllAsync(userId, dataType)).ReturnsAsync(products);
            _mapperMock.Setup(mapper => mapper.Map<List<ProductWithCategoryDTO>>(products)).Returns(productDTOs);

            // Act
            var result = await _productService.GetAllProductsWithCategoryAsync(userId, dataType);

            // Assert
            Assert.Equal(productDTOs, result);
        }

        [Theory]
        [InlineData(DataType.Public)]
        [InlineData(DataType.Private)]
        [InlineData(DataType.Both)]
        public async Task GetAllProductsWithDetailsAsync_ShouldReturnMappedProducts(DataType dataType)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var products = new List<Product> { new Product() };
            var productDTOs = new List<ProductWithDetailsDTO> { new ProductWithDetailsDTO() };

            _productRepositoryMock.Setup(repo => repo.GetAllAsync(userId, dataType)).ReturnsAsync(products);
            _mapperMock.Setup(mapper => mapper.Map<List<ProductWithDetailsDTO>>(products)).Returns(productDTOs);

            // Act
            var result = await _productService.GetAllProductsWithDetailsAsync(userId, dataType);

            // Assert
            Assert.Equal(productDTOs, result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnMappedProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var product = new Product();
            var productDTO = new ProductDTO();

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId, userId)).ReturnsAsync(product);
            _mapperMock.Setup(mapper => mapper.Map<ProductDTO>(product)).Returns(productDTO);

            // Act
            var result = await _productService.GetProductByIdAsync(productId, userId);

            // Assert
            Assert.Equal(productDTO, result);
        }

        [Fact]
        public async Task GetProductByIdWithMacroAsync_ShouldReturnMappedProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var product = new Product();
            var productDTO = new ProductWithMacroDTO();

            _productRepositoryMock.Setup(repo => repo.GetByIdWithDetailsAsync(productId, userId)).ReturnsAsync(product);
            _mapperMock.Setup(mapper => mapper.Map<ProductWithMacroDTO>(product)).Returns(productDTO);

            // Act
            var result = await _productService.GetProductByIdWithMacroAsync(productId, userId);

            // Assert
            Assert.Equal(productDTO, result);
        }

        [Fact]
        public async Task GetProductByIdWithCategoryAsync_ShouldReturnMappedProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var product = new Product();
            var productDTO = new ProductWithCategoryDTO();

            _productRepositoryMock.Setup(repo => repo.GetByIdWithDetailsAsync(productId, userId)).ReturnsAsync(product);
            _mapperMock.Setup(mapper => mapper.Map<ProductWithCategoryDTO>(product)).Returns(productDTO);

            // Act
            var result = await _productService.GetProductByIdWithCategoryAsync(productId, userId);

            // Assert
            Assert.Equal(productDTO, result);
        }

        [Fact]
        public async Task GetProductByIdWithDetailsAsync_ShouldReturnMappedProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var product = new Product();
            var productDTO = new ProductWithDetailsDTO();

            _productRepositoryMock.Setup(repo => repo.GetByIdWithDetailsAsync(productId, userId)).ReturnsAsync(product);
            _mapperMock.Setup(mapper => mapper.Map<ProductWithDetailsDTO>(product)).Returns(productDTO);

            // Act
            var result = await _productService.GetProductByIdWithDetailsAsync(productId, userId);

            // Assert
            Assert.Equal(productDTO, result);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldInvokeRepositoryCreate()
        {
            // Arrange
            var productDTO = new ProductCreateDTO
            {
                Name = "Test Product",
                Importance = ImportanceEnum.Medium,
                Packaged = true,
                UnitWeight = 100,
                ProductTypeId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Macro = new MacroCreateDTO { Calories = 100 }
            };
            var userId = Guid.NewGuid();
            var product = new Product();
            var macro = new Macro();
            var user = new User();
            var productType = new ProductType();
            var category = new Category();

            _mapperMock.Setup(mapper => mapper.Map<Product>(productDTO)).Returns(product);
            _mapperMock.Setup(mapper => mapper.Map<Macro>(productDTO.Macro)).Returns(macro);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _productTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(productDTO.ProductTypeId, userId))
                .ReturnsAsync(productType);
            _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(productDTO.CategoryId.Value, userId))
                .ReturnsAsync(category);

            // Act
            await _productService.CreateProductAsync(productDTO, userId);

            // Assert
            _productRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Product>(), userId), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldInvokeRepositoryUpdate()
        {
            // Arrange
            var productDTO = new ProductUpdateDTO
            {
                Id = Guid.NewGuid(),
                Name = "Updated Product",
                Importance = ImportanceEnum.High,
                Packaged = false,
                UnitWeight = 200,
                ProductTypeId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Macro = new MacroCreateDTO { Calories = 200 }
            };
            var userId = Guid.NewGuid();
            var product = new Product();
            var macro = new Macro();
            var user = new User();
            var productType = new ProductType();
            var category = new Category();

            _mapperMock.Setup(mapper => mapper.Map<Product>(productDTO)).Returns(product);
            _mapperMock.Setup(mapper => mapper.Map<Macro>(productDTO.Macro)).Returns(macro);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _productTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(productDTO.ProductTypeId, userId))
                .ReturnsAsync(productType);
            _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(productDTO.CategoryId.Value, userId))
                .ReturnsAsync(category);

            // Act
            await _productService.UpdateProductAsync(productDTO, userId);

            // Assert
            _productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>(), userId), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldInvokeRepositoryDelete()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            await _productService.DeleteProductAsync(productId);

            // Assert
            _productRepositoryMock.Verify(repo => repo.DeleteAsync(productId), Times.Once);
        }
    }
}