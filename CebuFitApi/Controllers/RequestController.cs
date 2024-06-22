using CebuFitApi.DTOs;
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
        
        if (userIdClaim != Guid.Empty)
        {
            if (userRole == RoleEnum.Admin || userRole == RoleEnum.Maintainer) 
            {
                var requests = await _requestService.GetAllRequestsAsync();
                if (requests.Count == 0)
                {
                    return NoContent();
                }
                return Ok(requests);
            }

            return Forbid();
        }
        return NotFound("User not found");
    }
}