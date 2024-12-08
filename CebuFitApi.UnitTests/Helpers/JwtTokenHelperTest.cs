using CebuFitApi.Helpers;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Helpers.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Xunit;

namespace CebuFitApi.UnitTests.Helpers;

[TestSubject(typeof(JwtTokenHelper))]
public class JwtTokenHelperTest
{
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly JwtTokenHelper _jwtTokenHelper;

    public JwtTokenHelperTest()
    {
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _jwtTokenHelper = new JwtTokenHelper(_httpContextAccessorMock.Object);
    }

    //[Theory]
    //[InlineData("User", false)]
    //[InlineData("Admin", true)]
    //[InlineData("Maintainer", null)]
    //public async Task GenerateJwtToken_ShouldReturnToken(string role, bool? expire)
    //{
    //    // Arrange
    //    var user = new User
    //    {
    //        Id = Guid.NewGuid(),
    //        Name = "Test User",
    //        Role = role
    //    };

    //    Environment.SetEnvironmentVariable("SSK", "supersecretkey");

    //    // Act
    //    var token = await _jwtTokenHelper.GenerateJwtToken(user, expire);

    //    // Assert
    //    Assert.NotNull(token);
    //    var handler = new JwtSecurityTokenHandler();
    //    var jwtToken = handler.ReadJwtToken(token);
    //    Assert.Equal("cebufit", jwtToken.Issuer);
    //    Assert.Equal("cebufitEater", jwtToken.Audiences.First());
    //    Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString());
    //    Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Name && c.Value == user.Name);
    //    Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Role && c.Value == user.Role);
    //}

    [Fact]
    public void GetCurrentUserId_ShouldReturnUserId_WhenClaimExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        var result = _jwtTokenHelper.GetCurrentUserId();

        // Assert
        Assert.Equal(userId, result);
    }

    [Fact]
    public void GetCurrentUserId_ShouldReturnEmptyGuid_WhenClaimDoesNotExist()
    {
        // Arrange
        var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal() };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        var result = _jwtTokenHelper.GetCurrentUserId();

        // Assert
        Assert.Equal(Guid.Empty, result);
    }

    [Theory]
    [InlineData("User", RoleEnum.User)]
    [InlineData("Admin", RoleEnum.Admin)]
    [InlineData("Maintainer", RoleEnum.Maintainer)]
    public void GetUserRole_ShouldReturnRole_WhenClaimExists(string role, RoleEnum expectedRole)
    {
        // Arrange
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        var result = _jwtTokenHelper.GetUserRole();

        // Assert
        Assert.Equal(expectedRole, result);
    }

    [Fact]
    public void GetUserRole_ShouldReturnNull_WhenClaimDoesNotExist()
    {
        // Arrange
        var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal() };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        var result = _jwtTokenHelper.GetUserRole();

        // Assert
        Assert.Null(result);
    }
}