﻿using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers;

[Authorize]
[ApiController]
[Route("/api/requests")]
public class RequestController : Controller
{
    private readonly IRequestService _requestService;
    private readonly IJwtTokenHelper _jwtTokenHelper;
    
    public RequestController(IRequestService requestService, IJwtTokenHelper jwtTokenHelper)
    {
        _requestService = requestService;
        _jwtTokenHelper = jwtTokenHelper;
    }
    
    [HttpGet(Name = "GetRequests")]
    public async Task<ActionResult<List<RequestDto>>> GetAll()
    {
        var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
        var userRole = _jwtTokenHelper.GetUserRole();
        
        if(userIdClaim == Guid.Empty) return NotFound("User not found");
        if (userRole != RoleEnum.Admin && userRole != RoleEnum.Maintainer) return Forbid();

        var requests = await _requestService.GetAllRequestsAsync();
        
        if (requests.Count == 0) return NoContent();
        return Ok(requests);
    }
    
    [HttpGet("byTypeAndStatus", Name = "GetRequestsByTypeAndStatus")]
    public async Task<ActionResult<List<RequestDto>>> GetRequestsByTypeAndStatus(
        [FromQuery] RequestType requestType,
        [FromQuery] RequestStatus requestStatus)
    {
        var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
        var userRole = _jwtTokenHelper.GetUserRole();
        
        if (userIdClaim == Guid.Empty) return NotFound("User not found");
        if (userRole != RoleEnum.Admin && userRole != RoleEnum.Maintainer) return Forbid();

        var requests = await _requestService.GetRequestsByTypeAndStatus(requestType, requestStatus);

        if (requests.Count == 0) return NoContent();
        return Ok(requests);

            
        
    }
    
    [HttpGet("ProductByStatusWithDetails", Name = "GetRequestsProductByStatusWithDetails")]
    public async Task<ActionResult<List<RequestProductWithDetailsDto>>> GetRequestsProductByStatusWithDetails(
        [FromQuery] RequestStatus requestStatus)
    {
        var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
        var userRole = _jwtTokenHelper.GetUserRole();
        
        if (userIdClaim == Guid.Empty) return NotFound("User not found");
        if (userRole != RoleEnum.Admin && userRole != RoleEnum.Maintainer) return Forbid();
        
        var requests = await _requestService.GetRequestsProductByStatusWithDetails(requestStatus);
            
        if (requests.Count == 0) return NoContent();
        return Ok(requests);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateRequest(RequestCreateDto requestDto)
    {
        var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
        
        if (userIdClaim == Guid.Empty) return NotFound("User not found");
        
        await _requestService.CreateRequestAsync(requestDto, userIdClaim);
        return Ok();

    }
    [HttpPut("ChangeStatus", Name = "ChangeRequestStatus")]
    public async Task<ActionResult> ChangeStatus(
        [FromQuery] Guid id,
        [FromQuery] RequestStatus requestStatus)
    {
        var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
        var userRole = _jwtTokenHelper.GetUserRole();
        
        if (userIdClaim == Guid.Empty) return NotFound("User not found");
        if (userRole != RoleEnum.Admin && userRole != RoleEnum.Maintainer) return Forbid();
        
        await _requestService.ChangeRequestStatusAsync(id, requestStatus, userIdClaim);
        return Ok();
    }
}