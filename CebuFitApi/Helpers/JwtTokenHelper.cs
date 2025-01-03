﻿using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.Helpers;

public class JwtTokenHelper : IJwtTokenHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public JwtTokenHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        DotNetEnv.Env.Load();
    }
    public async Task<string> GenerateJwtToken(User user, bool? expire)
    {
        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SSK")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        DateTime? expires;
        if (expire != null && expire.HasValue && !expire.Value) expires = DateTime.MaxValue;
        else expires = DateTime.Now.AddHours(2);

        var token = new JwtSecurityToken(
        issuer: "cebufit",
        audience: "cebufitEater",
        claims: claims,
        expires: expires,
        signingCredentials: credentials
    );
        var newToken = new JwtSecurityTokenHandler().WriteToken(token);
        return newToken;
    }

    public Guid GetCurrentUserId()
    {
        // Extract user ID from the JWT token
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }

        // Default value or handle the case where the user ID is not available
        return Guid.Empty;
    }
    
    public RoleEnum? GetUserRole()
    {
        var roleClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

        if (roleClaim != null && Enum.TryParse<RoleEnum>(roleClaim.Value, out var role))
        {
            return role;
        }
        return null;
    }
}

