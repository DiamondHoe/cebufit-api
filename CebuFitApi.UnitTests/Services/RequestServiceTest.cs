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
    [TestSubject(typeof(RequestService))]
    public class RequestServiceTest
    {
        private readonly Mock<IRequestRepository> _mockRequestRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IRecipeRepository> _mockRecipeRepository;
        private readonly Mock<IProductTypeRepository> _mockProductTypeRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RequestService _requestService;

        public RequestServiceTest()
        {
            _mockRequestRepository = new Mock<IRequestRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockRecipeRepository = new Mock<IRecipeRepository>();
            _mockProductTypeRepository = new Mock<IProductTypeRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();

            _requestService = new RequestService(
                _mockRequestRepository.Object,
                _mockProductRepository.Object,
                _mockRecipeRepository.Object,
                _mockProductTypeRepository.Object,
                _mockCategoryRepository.Object,
                _mockUserRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllRequestsAsync_ShouldReturnMappedRequestDtos()
        {
            // Arrange
            var requests = new List<Request> { new Request() };
            _mockRequestRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(requests);
            _mockMapper.Setup(m => m.Map<List<RequestDto>>(requests)).Returns(new List<RequestDto>());

            // Act
            var result = await _requestService.GetAllRequestsAsync();

            // Assert
            Assert.NotNull(result);
            _mockMapper.Verify(m => m.Map<List<RequestDto>>(requests), Times.Once);
        }

        [Fact]
        public async Task GetRequestsByStatus_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotImplementedException>(() =>
                _requestService.GetRequestsByStatus(RequestStatus.Pending));
        }

        [Fact]
        public async Task GetRequestsByType_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotImplementedException>(() =>
                _requestService.GetRequestsByType(RequestType.PromoteProductToPublic));
        }

        [Fact]
        public async Task GetRequestsByTypeAndStatus_ShouldReturnMappedRequestDtos()
        {
            // Arrange
            var requests = new List<Request> { new Request() };
            _mockRequestRepository
                .Setup(repo => repo.GetByTypeAndStatus(RequestType.PromoteProductToPublic, RequestStatus.Pending))
                .ReturnsAsync(requests);
            _mockMapper.Setup(m => m.Map<List<RequestDto>>(requests)).Returns(new List<RequestDto>());

            // Act
            var result =
                await _requestService.GetRequestsByTypeAndStatus(RequestType.PromoteProductToPublic,
                    RequestStatus.Pending);

            // Assert
            Assert.NotNull(result);
            _mockMapper.Verify(m => m.Map<List<RequestDto>>(requests), Times.Once);
        }

        //[Fact]
        //public async Task GetRequestsProductByStatusWithDetails_ShouldReturnMappedRequestProductWithDetailsDtos()
        //{
        //    // Arrange
        //    var requests = new List<Request> { new Request() };
        //    _mockRequestRepository
        //        .Setup(repo => repo.GetByTypeAndStatus(RequestType.PromoteProductToPublic, RequestStatus.Pending))
        //        .ReturnsAsync(requests);
        //    _mockMapper.Setup(m => m.Map<List<RequestProductWithDetailsDto>>(requests))
        //        .Returns(new List<RequestProductWithDetailsDto>());

        //    // Act
        //    var result = await _requestService.GetRequestsProductByStatusWithDetails(RequestStatus.Pending);

        //    // Assert
        //    Assert.NotNull(result);
        //    _mockMapper.Verify(m => m.Map<List<RequestProductWithDetailsDto>>(requests), Times.Once);
        //}

        //[Fact]
        //public async Task GetRequestsRecipeByStatusWithDetails_ShouldReturnMappedRequestRecipeWithDetailsDtos()
        //{
        //    // Arrange
        //    var requests = new List<Request> { new Request() };
        //    _mockRequestRepository
        //        .Setup(repo => repo.GetByTypeAndStatus(RequestType.PromoteRecipeToPublic, RequestStatus.Pending))
        //        .ReturnsAsync(requests);
        //    _mockMapper.Setup(m => m.Map<List<RequestRecipeWithDetailsDto>>(requests))
        //        .Returns(new List<RequestRecipeWithDetailsDto>());

        //    // Act
        //    var result = await _requestService.GetRequestsRecipeByStatusWithDetails(RequestStatus.Pending);

        //    // Assert
        //    Assert.NotNull(result);
        //    _mockMapper.Verify(m => m.Map<List<RequestRecipeWithDetailsDto>>(requests), Times.Once);
        //}

        //[Fact]
        //public async Task GetRequestsProductTypeByStatus_ShouldReturnMappedRequestProductTypeDtos()
        //{
        //    // Arrange
        //    var requests = new List<Request> { new Request() };
        //    _mockRequestRepository
        //        .Setup(repo => repo.GetByTypeAndStatus(RequestType.PromoteProductTypeToPublic, RequestStatus.Pending))
        //        .ReturnsAsync(requests);
        //    _mockMapper.Setup(m => m.Map<List<RequestProductTypeDto>>(requests))
        //        .Returns(new List<RequestProductTypeDto>());

        //    // Act
        //    var result = await _requestService.GetRequestsProductTypeByStatus(RequestStatus.Pending);

        //    // Assert
        //    Assert.NotNull(result);
        //    _mockMapper.Verify(m => m.Map<List<RequestProductTypeDto>>(requests), Times.Once);
        //}

        //[Fact]
        //public async Task GetRequestsCategoriesByStatus_ShouldReturnMappedRequestCategoryDtos()
        //{
        //    // Arrange
        //    var requests = new List<Request> { new Request() };
        //    _mockRequestRepository
        //        .Setup(repo => repo.GetByTypeAndStatus(RequestType.PromoteCategoryToPublic, RequestStatus.Pending))
        //        .ReturnsAsync(requests);
        //    _mockMapper.Setup(m => m.Map<List<RequestCategoryDto>>(requests)).Returns(new List<RequestCategoryDto>());

        //    // Act
        //    var result = await _requestService.GetRequestsCategoriesByStatus(RequestStatus.Pending);

        //    // Assert
        //    Assert.NotNull(result);
        //    _mockMapper.Verify(m => m.Map<List<RequestCategoryDto>>(requests), Times.Once);
        //}

        [Fact]
        public async Task GetRequestByIdAsync_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotImplementedException>(() =>
                _requestService.GetRequestByIdAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task CreateRequestAsync_ShouldReturnTrueIfRequestCreated()
        {
            // Arrange
            var requestCreateDto = new RequestCreateDto();
            var userId = Guid.NewGuid();
            var request = new Request();
            _mockMapper.Setup(m => m.Map<Request>(requestCreateDto)).Returns(request);
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(new User());
            _mockRequestRepository.Setup(repo => repo.CreateAsync(request)).ReturnsAsync(true);

            // Act
            var result = await _requestService.CreateRequestAsync(requestCreateDto, userId);

            // Assert
            Assert.True(result);
            _mockRequestRepository.Verify(repo => repo.CreateAsync(request), Times.Once);
        }

        [Fact]
        public async Task UpdateRequestAsync_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotImplementedException>(() =>
                _requestService.UpdateRequestAsync(new RequestDto(), Guid.NewGuid()));
        }

        [Fact]
        public async Task DeleteRequestAsync_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotImplementedException>(() =>
                _requestService.DeleteRequestAsync(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task ChangeRequestStatusAsync_ShouldUpdateRequestStatus()
        {
            // Arrange
            var requestId = Guid.NewGuid();
            var requestStatus = RequestStatus.Approved;
            var userId = Guid.NewGuid();
            var request = new Request
                { Type = RequestType.PromoteProductToPublic, RequestedItemId = Guid.NewGuid(), Requester = new User() };
            var product = new Product();
            _mockRequestRepository.Setup(repo => repo.GetByIdAsync(requestId)).ReturnsAsync(request);
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(new User());
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(request.RequestedItemId, request.Requester.Id))
                .ReturnsAsync(product);

            // Act
            await _requestService.ChangeRequestStatusAsync(requestId, requestStatus, userId);

            // Assert
            Assert.Equal(requestStatus, request.Status);
            _mockRequestRepository.Verify(repo => repo.UpdateAsync(request), Times.Once);
        }
    }
}