using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public UserController(IUserService userService, IJwtTokenHelper jwtTokenHelper)
        {
            _userService = userService;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO user, bool? expire = true)
        {
            var loggedUser = await _userService.AuthenticateAsync(user);

            if (loggedUser == null)
            {
                return Unauthorized("Invalid credentials");
            }
            var token = await _jwtTokenHelper.GenerateJwtToken(loggedUser, expire);

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserCreateDTO? registerUser)
        {
            if (registerUser == null) return BadRequest();
            registerUser.Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
            
            var (registerSuccess, userEntity) = await _userService.CreateAsync(registerUser);
            if (!registerSuccess) return Conflict("Name is already taken");

            var token = await _jwtTokenHelper.GenerateJwtToken(userEntity, true);
            return Ok(new { Token = token });
            
        }

        [Authorize]
        [HttpGet("summary")]
        public async Task<ActionResult<SummaryDTO>> GetSummaryAsync(DateTime start, DateTime end)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim != Guid.Empty)
            {
                var summaryData = await _userService.GetSummaryAsync(userIdClaim, start, end);
                return Ok(summaryData);
            }
    
            return NotFound("User not found");
        }

        // TODO research jak wysyłać maila z przypomnieniem hasła, póki co sam mail
        //[HttpGet]
        //public async Task<ActionResult> ResetPassword(string email)
        //{

        //}

        //NP: Can be implemented, but for now we do it on client side - if needed i'll add blacklisting
        //[Authorize]
        //[HttpPost("logout")]
        ////[Authorize]
        //public IActionResult Logout()
        //{
        //    // You can add additional logout logic here if needed
        //    return Ok("Logout successful");
        //}
    }
}
