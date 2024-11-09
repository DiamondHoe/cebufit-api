using AutoMapper;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/userDemand")]
    public class UserDemandController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IJwtTokenHelper _jwtTokenHelper;
        private readonly IUserDemandService _demandService;
        public UserDemandController(
            IMapper mapper,
            IJwtTokenHelper jwtTokenHelper,
            IUserDemandService demandService)
        {
            _mapper = mapper;
            _jwtTokenHelper = jwtTokenHelper;
            _demandService = demandService;

        }
        [HttpGet(Name = "GetDemand")]
        public async Task<ActionResult<UserDemandDTO>> GetDemand()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var demand = await _demandService.GetDemandAsync(userIdClaim);
                if (demand == null)
                {
                    return NoContent();
                }
                return Ok(demand);
            }
            return NotFound("User not found");
        }
        [HttpPut(Name = "UpdateDemand")]
        public async Task<ActionResult> UpdateDemand(UserDemandUpdateDTO demandUpdateDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim != Guid.Empty)
            {
                var demand = await _demandService.GetDemandAsync(userIdClaim);
                if (demand == null)
                {
                    return NotFound();
                }
                await _demandService.UpdateDemandAsync(demandUpdateDTO, userIdClaim);
                return Ok();
            }
            return NotFound("User not found");
        }
        [HttpGet("calulate/auto", Name = "AutoCalculateDemand")]
        public async Task<ActionResult> AutoCalculateDemand()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim == Guid.Empty) return NotFound("User not found");
            
            await _demandService.AutoCalculateDemandAsync(userIdClaim);
            return Ok();
        }
    }
}
