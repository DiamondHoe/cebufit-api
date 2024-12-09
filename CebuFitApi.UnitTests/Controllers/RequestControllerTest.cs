using CebuFitApi.Controllers;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CebuFitApi.Helpers;
using JetBrains.Annotations;
using Xunit;

namespace CebuFitApi.UnitTests.Controllers;

[TestSubject(typeof(RequestController))]
public class RequestControllerTest
{
    private readonly Mock<IRequestService> _mockRequestService;
    private readonly Mock<IJwtTokenHelper> _mockJwtTokenHelper;
    private readonly Mock<WebSocketHandler> _mockWebSocketHandler;
    private readonly RequestController _controller;

    public RequestControllerTest()
    {
        _mockRequestService = new Mock<IRequestService>();
        _mockJwtTokenHelper = new Mock<IJwtTokenHelper>();
        _mockWebSocketHandler = new Mock<WebSocketHandler>();
        _controller = new RequestController(_mockRequestService.Object, _mockJwtTokenHelper.Object,
            _mockWebSocketHandler.Object);
    }

    [Fact]
    public async Task GetAll_UserNotFound_ReturnsNotFound()
    {
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        var result = await _controller.GetAll();

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_UserNotAuthorized_ReturnsForbid()
    {
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
        _mockJwtTokenHelper.Setup(x => x.GetUserRole()).Returns(RoleEnum.User);

        var result = await _controller.GetAll();

        Assert.IsType<ForbidResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_NoRequests_ReturnsNoContent()
    {
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
        _mockJwtTokenHelper.Setup(x => x.GetUserRole()).Returns(RoleEnum.Admin);
        _mockRequestService.Setup(x => x.GetAllRequestsAsync()).ReturnsAsync(new List<RequestDto>());

        var result = await _controller.GetAll();

        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_RequestsExist_ReturnsOk()
    {
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
        _mockJwtTokenHelper.Setup(x => x.GetUserRole()).Returns(RoleEnum.Admin);
        _mockRequestService.Setup(x => x.GetAllRequestsAsync()).ReturnsAsync(new List<RequestDto> { new RequestDto() });

        var result = await _controller.GetAll();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Theory]
    [InlineData(RequestType.PromoteProductToPublic, RequestStatus.Pending)]
    [InlineData(RequestType.PromoteRecipeToPublic, RequestStatus.Approved)]
    [InlineData(RequestType.PromoteCategoryToPublic, RequestStatus.Rejected)]
    public async Task GetRequestsByTypeAndStatus_ValidRequest_ReturnsOk(RequestType requestType,
        RequestStatus requestStatus)
    {
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
        _mockJwtTokenHelper.Setup(x => x.GetUserRole()).Returns(RoleEnum.Admin);
        _mockRequestService.Setup(x => x.GetRequestsByTypeAndStatus(requestType, requestStatus))
            .ReturnsAsync(new List<RequestDto> { new RequestDto() });

        var result = await _controller.GetRequestsByTypeAndStatus(requestType, requestStatus);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateRequest_UserNotFound_ReturnsNotFound()
    {
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.Empty);

        var result = await _controller.CreateRequest(new RequestCreateDto());

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task CreateRequest_RequestAlreadyExists_ReturnsConflict()
    {
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
        _mockRequestService.Setup(x => x.CreateRequestAsync(It.IsAny<RequestCreateDto>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await _controller.CreateRequest(new RequestCreateDto());

        Assert.IsType<ConflictObjectResult>(result);
    }

    [Fact]
    public async Task CreateRequest_ValidRequest_ReturnsOk()
    {
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
        _mockRequestService.Setup(x => x.CreateRequestAsync(It.IsAny<RequestCreateDto>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await _controller.CreateRequest(new RequestCreateDto());

        Assert.IsType<OkResult>(result);
    }

    [Theory]
    [InlineData(RequestStatus.Pending)]
    [InlineData(RequestStatus.Approved)]
    [InlineData(RequestStatus.Rejected)]
    public async Task ChangeStatus_ValidRequest_ReturnsOk(RequestStatus requestStatus)
    {
        _mockJwtTokenHelper.Setup(x => x.GetCurrentUserId()).Returns(Guid.NewGuid());
        _mockJwtTokenHelper.Setup(x => x.GetUserRole()).Returns(RoleEnum.Admin);

        var result = await _controller.ChangeStatus(Guid.NewGuid(), requestStatus);

        Assert.IsType<OkResult>(result);
    }
}