using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
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
        public async Task<ActionResult> Login(UserLoginDTO user)
        {
            var loggedUser = await _userService.AuthenticateAsync(user);

            if (loggedUser == null)
            {
                return Unauthorized("Invalid credentials");
            }
            var token = await _jwtTokenHelper.GenerateJwtToken(loggedUser.Id, loggedUser.Name);

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserCreateDTO registerUser)
        {
            if (registerUser == null)
            {
                return BadRequest();
            }
            registerUser.Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
            var isRegistered = await _userService.CreateAsync(registerUser);

            if(isRegistered)
            {
                return Ok("Registration successful");
            }
            return BadRequest();
        }

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
