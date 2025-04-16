using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using server.Data;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController(
    ApplicationDbContext db,
    SigningCredentials credentials,
    IPasswordHasher<User> hasher,
    IConfiguration configuration)
    : ControllerBase
{
    // POST at /api/Auth takes in {"username":"", "password":""}, and returns a JWT token if credentials are valid
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username == loginCredentials.Username);
        if (user == null) return NotFound($"User {loginCredentials.Username} not found");

        // user.PasswordHash is never null since, it's returned from a database, where it is a required element
        // in any case the server would just return a 500 error if it was in fact null
        var result = hasher.VerifyHashedPassword(
            user,
            user.PasswordHash!,
            loginCredentials.Password
        );
        if (result != PasswordVerificationResult.Success) return Unauthorized("invalid password");

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
        var expirationTime = int.Parse(configuration["Jwt:ExpireMinutes"]!);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationTime),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new { token = tokenString });
    }

    // used in parsing the form at POST /api/Auth
    public record LoginCredentials(string Username, string Password);
}