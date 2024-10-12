using AutoMapper;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/demand")]
    public class DemandController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IJwtTokenHelper _jwtTokenHelper;
        private readonly IDemandService _demandService;
        public DemandController(
            IMapper mapper,
            IJwtTokenHelper jwtTokenHelper,
            IDemandService demandService)
        {
            _mapper = mapper;
            _jwtTokenHelper = jwtTokenHelper;
            _demandService = demandService;

        }
        [HttpGet(Name = "GetDemand")]
        public async Task<ActionResult<DemandDTO>> GetDemand()
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
        public async Task<ActionResult> UpdateDemand(DemandUpdateDTO demandUpdateDTO)
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
            if (userIdClaim != Guid.Empty)
            {
                await _demandService.AutoCalculateDemandAsync(userIdClaim);
                return Ok();
            }
            return NotFound("User not found");
        }
    }
}
