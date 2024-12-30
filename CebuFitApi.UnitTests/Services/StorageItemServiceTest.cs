using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Services;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace CebuFitApi.UnitTests.Services
{
    [TestSubject(typeof(StorageItemService))]
    public class StorageItemServiceTest
    {
        private readonly Mock<IStorageItemRepository> _storageItemRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly StorageItemService _storageItemService;

        public StorageItemServiceTest()
        {
            _storageItemRepositoryMock = new Mock<IStorageItemRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _storageItemService = new StorageItemService(
                _mapperMock.Object,
                _storageItemRepositoryMock.Object,
                _productRepositoryMock.Object,
                _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllStorageItemsAsync_ShouldReturnStorageItems()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var storageItems = new List<StorageItem> { new StorageItem() };
            _storageItemRepositoryMock.Setup(repo => repo.GetAllAsync(userId)).ReturnsAsync(storageItems);
            _mapperMock.Setup(mapper => mapper.Map<List<StorageItemDTO>>(storageItems))
                .Returns(new List<StorageItemDTO>());

            // Act
            var result = await _storageItemService.GetAllStorageItemsAsync(userId);

            // Assert
            Assert.NotNull(result);
            _storageItemRepositoryMock.Verify(repo => repo.GetAllAsync(userId), Times.Once);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GetAllStorageItemsWithProductAsync_ShouldReturnStorageItemsWithProduct(bool withoutEaten)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var storageItems = new List<StorageItem> { new StorageItem() };
            _storageItemRepositoryMock.Setup(repo => repo.GetAllWithProductAsync(userId, withoutEaten))
                .ReturnsAsync(storageItems);
            _mapperMock.Setup(mapper => mapper.Map<List<StorageItemWithProductDTO>>(storageItems))
                .Returns(new List<StorageItemWithProductDTO>());

            // Act
            var result = await _storageItemService.GetAllStorageItemsWithProductAsync(userId, withoutEaten);

            // Assert
            Assert.NotNull(result);
            _storageItemRepositoryMock.Verify(repo => repo.GetAllWithProductAsync(userId, withoutEaten), Times.Once);
        }

        [Fact]
        public async Task GetAllStorageItemsByProductIdWithProductAsync_ShouldReturnStorageItemsByProductId()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var storageItems = new List<StorageItem> { new StorageItem() };
            _storageItemRepositoryMock.Setup(repo => repo.GetAllByProductIdWithProductAsync(productId, userId))
                .ReturnsAsync(storageItems);
            _mapperMock.Setup(mapper => mapper.Map<List<StorageItemWithProductDTO>>(storageItems))
                .Returns(new List<StorageItemWithProductDTO>());

            // Act
            var result = await _storageItemService.GetAllStorageItemsByProductIdWithProductAsync(productId, userId);

            // Assert
            Assert.NotNull(result);
            _storageItemRepositoryMock.Verify(repo => repo.GetAllByProductIdWithProductAsync(productId, userId),
                Times.Once);
        }

        [Fact]
        public async Task GetStorageItemByIdAsync_ShouldReturnStorageItem()
        {
            // Arrange
            var storageItemId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var storageItem = new StorageItem();
            _storageItemRepositoryMock.Setup(repo => repo.GetByIdAsync(storageItemId, userId))
                .ReturnsAsync(storageItem);
            _mapperMock.Setup(mapper => mapper.Map<StorageItemDTO>(storageItem)).Returns(new StorageItemDTO());

            // Act
            var result = await _storageItemService.GetStorageItemByIdAsync(storageItemId, userId);

            // Assert
            Assert.NotNull(result);
            _storageItemRepositoryMock.Verify(repo => repo.GetByIdAsync(storageItemId, userId), Times.Once);
        }

        [Fact]
        public async Task GetStorageItemByIdWithProductAsync_ShouldReturnStorageItemWithProduct()
        {
            // Arrange
            var storageItemId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var storageItem = new StorageItem();
            _storageItemRepositoryMock.Setup(repo => repo.GetByIdWithProductAsync(storageItemId, userId))
                .ReturnsAsync(storageItem);
            _mapperMock.Setup(mapper => mapper.Map<StorageItemWithProductDTO>(storageItem))
                .Returns(new StorageItemWithProductDTO());

            // Act
            var result = await _storageItemService.GetStorageItemByIdWithProductAsync(storageItemId, userId);

            // Assert
            Assert.NotNull(result);
            _storageItemRepositoryMock.Verify(repo => repo.GetByIdWithProductAsync(storageItemId, userId), Times.Once);
        }

        [Fact]
        public async Task CreateStorageItemAsync_ShouldCreateStorageItem()
        {
            // Arrange
            var storageItemDTO = new StorageItemCreateDTO { baseProductId = Guid.NewGuid() };
            var userId = Guid.NewGuid();
            var storageItem = new StorageItem();
            var user = new User();
            var product = new Product { UnitWeight = 1 };
            _mapperMock.Setup(mapper => mapper.Map<StorageItem>(storageItemDTO)).Returns(storageItem);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(storageItemDTO.baseProductId, userId))
                .ReturnsAsync(product);

            // Act
            await _storageItemService.CreateStorageItemAsync(storageItemDTO, userId);

            // Assert
            _storageItemRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<StorageItem>(), userId), Times.Once);
        }

        [Fact]
        public async Task UpdateStorageItemAsync_ShouldUpdateStorageItem()
        {
            // Arrange
            var storageItemDTO = new StorageItemDTO();
            var userId = Guid.NewGuid();
            var storageItem = new StorageItem();
            var user = new User();
            _mapperMock.Setup(mapper => mapper.Map<StorageItem>(storageItemDTO)).Returns(storageItem);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            await _storageItemService.UpdateStorageItemAsync(storageItemDTO, userId);

            // Assert
            _storageItemRepositoryMock.Verify(repo => repo.UpdateAsync(storageItem, userId), Times.Once);
        }

        [Fact]
        public async Task DeleteStorageItemAsync_ShouldDeleteStorageItem()
        {
            // Arrange
            var storageItemId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            await _storageItemService.DeleteStorageItemAsync(storageItemId, userId);

            // Assert
            _storageItemRepositoryMock.Verify(repo => repo.DeleteAsync(storageItemId, userId), Times.Once);
        }
    }
}