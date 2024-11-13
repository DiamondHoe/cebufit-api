using CebuFitApi.DTOs;
using CebuFitApi.Helpers;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CebuFitApi.Controllers;

[Authorize]
[ApiController]
[Route("/api/requests")]
public class RequestController : Controller
{
    private readonly IRequestService _requestService;
    private readonly IJwtTokenHelper _jwtTokenHelper;
    private readonly WebSocketHandler _webSocketHandler;

    public RequestController(
        IRequestService requestService, 
        IJwtTokenHelper jwtTokenHelper, 
        WebSocketHandler webSocketHandler
        )
    {
        _requestService = requestService;
        _jwtTokenHelper = jwtTokenHelper;
        _webSocketHandler = webSocketHandler;
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
    
    [HttpGet("RecipeByStatusWithDetails", Name = "GetRequestsRecipeByStatusWithDetails")]
    public async Task<ActionResult<List<RequestRecipeWithDetailsDto>>> GetRequestsRecipeByStatusWithDetails(
        [FromQuery] RequestStatus requestStatus)
    {
        var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
        var userRole = _jwtTokenHelper.GetUserRole();
        
        if (userIdClaim == Guid.Empty) return NotFound("User not found");
        if (userRole != RoleEnum.Admin && userRole != RoleEnum.Maintainer) return Forbid();
        
        var requests = await _requestService.GetRequestsRecipeByStatusWithDetails(requestStatus);
            
        if (requests.Count == 0) return NoContent();
        return Ok(requests);
    }
    
    [HttpGet("ProductTypeByStatusWithDetails", Name = "GetRequestsProductTypeByStatusWithDetails")]
    public async Task<ActionResult<List<RequestProductWithDetailsDto>>> GetRequestsProductTypeByStatusWithDetails(
        [FromQuery] RequestStatus requestStatus)
    {
        var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
        var userRole = _jwtTokenHelper.GetUserRole();
        
        if (userIdClaim == Guid.Empty) return NotFound("User not found");
        if (userRole != RoleEnum.Admin && userRole != RoleEnum.Maintainer) return Forbid();
        
        var requests = await _requestService.GetRequestsProductTypeByStatusWithDetails(requestStatus);
            
        if (requests.Count == 0) return NoContent();
        return Ok(requests);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateRequest(RequestCreateDto requestDto)
    {
        var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
        
        if (userIdClaim == Guid.Empty) return NotFound("User not found");
        
        bool requestAlreadyCreated = await _requestService.CreateRequestAsync(requestDto, userIdClaim);
        if (requestAlreadyCreated)
        {
            return Conflict("Request already created for this item.");
        }
        await _webSocketHandler.BroadcastMessageAsync(new { Message = "Request added" });
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

        if(requestStatus == RequestStatus.Rejected)
            await _webSocketHandler.BroadcastMessageAsync(new { Message = "Request rejected" });
        if(requestStatus == RequestStatus.Approved) 
            await _webSocketHandler.BroadcastMessageAsync(new { Message = "Request approved" });

        return Ok();
    }
}