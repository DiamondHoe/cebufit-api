using CebuFitApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi
{
    [ApiController]
    [Route("/api/health")]
    public class HealthCheck : Controller
    {
        [HttpGet(Name = "check")]
        public async Task<ActionResult<List<MealDTO>>> Check()
        {
            return Ok("Alive");
        }
    }
}
