using CebuFitApi.Controllers;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace CebuFitApi.UnitTests.Controllers
{
    [TestSubject(typeof(CategoryController))]
    public class CategoryControllerTest
    {
        private readonly Mock<ILogger<CategoryController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly Mock<IJwtTokenHelper> _jwtTokenHelperMock;
        private readonly CategoryController _controller;

        public CategoryControllerTest()
        {
            _loggerMock = new Mock<ILogger<CategoryController>>();
            _mapperMock = new Mock<IMapper>();
            _categoryServiceMock = new Mock<ICategoryService>();
            _jwtTokenHelperMock = new Mock<IJwtTokenHelper>();
            _controller = new CategoryController(_loggerMock.Object, _categoryServiceMock.Object, _mapperMock.Object,
                _jwtTokenHelperMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound_WhenUserIdIsEmpty()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            var result = await _controller.GetAll();

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_ReturnsNoContent_WhenNoCategoriesFound()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
            _categoryServiceMock.Setup(x => x.GetAllCategoriesAsync(It.IsAny<Guid>(), It.IsAny<DataType>()))
                .ReturnsAsync(new List<CategoryDTO>());

            var result = await _controller.GetAll();

            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WhenCategoriesFound()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
            _categoryServiceMock.Setup(x => x.GetAllCategoriesAsync(It.IsAny<Guid>(), It.IsAny<DataType>()))
                .ReturnsAsync(new List<CategoryDTO> { new CategoryDTO() });

            var result = await _controller.GetAll();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenUserIdIsEmpty()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            var result = await _controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenCategoryNotFound()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
            _categoryServiceMock.Setup(x => x.GetCategoryByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((CategoryDTO)null);

            var result = await _controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenCategoryFound()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
            _categoryServiceMock.Setup(x => x.GetCategoryByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new CategoryDTO());

            var result = await _controller.GetById(Guid.NewGuid());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateCategory_ReturnsNotFound_WhenUserIdIsEmpty()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            var result = await _controller.CreateCategory(new CategoryCreateDTO());

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateCategory_ReturnsBadRequest_WhenCategoryCreateDTOIsNull()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());

            var result = await _controller.CreateCategory(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateCategory_ReturnsOk_WhenCategoryCreated()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());

            var result = await _controller.CreateCategory(new CategoryCreateDTO());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsNotFound_WhenUserIdIsEmpty()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            var result = await _controller.UpdateCategory(new CategoryDTO());

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsNotFound_WhenCategoryNotFound()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
            _categoryServiceMock.Setup(x => x.GetCategoryByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((CategoryDTO)null);

            var result = await _controller.UpdateCategory(new CategoryDTO());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsOk_WhenCategoryUpdated()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
            _categoryServiceMock.Setup(x => x.GetCategoryByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new CategoryDTO());

            var result = await _controller.UpdateCategory(new CategoryDTO());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsNotFound_WhenUserIdIsEmpty()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

            var result = await _controller.DeleteCategory(Guid.NewGuid());

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsNotFound_WhenCategoryNotFound()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
            _categoryServiceMock.Setup(x => x.GetCategoryByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((CategoryDTO)null);

            var result = await _controller.DeleteCategory(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsOk_WhenCategoryDeleted()
        {
            _jwtTokenHelperMock.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
            _categoryServiceMock.Setup(x => x.GetCategoryByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new CategoryDTO());

            var result = await _controller.DeleteCategory(Guid.NewGuid());

            Assert.IsType<OkResult>(result);
        }
    }
}