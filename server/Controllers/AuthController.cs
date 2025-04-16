using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.Data;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController(
    ApplicationDbContext db,
    SigningCredentials credentials,
    IPasswordHasher<User> hasher)
    : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginCredentials loginCredentials)
    {
        var user = db.Users.FirstOrDefault(u => u.Username == loginCredentials.Username);
        if (user == null) return NotFound($"User {loginCredentials.Username} not found");

        // should never happen, and this property is required in database, and so every query should return it
        if (user.PasswordHash == null)
            return NotFound($"Couldn't find password hash for user {loginCredentials.Username}");

        var result = hasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            loginCredentials.Password
        );
        if (result != PasswordVerificationResult.Success) return Unauthorized("invalid password");

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

        var token = new JwtSecurityToken(
            /*claims not strictly necessary*/
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = tokenString });
    }

    public class LoginCredentials
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
}