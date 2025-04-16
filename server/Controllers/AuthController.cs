using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly SigningCredentials _credentials;
    private readonly IPasswordHasher<User> _hasher;

    public AuthController(
        ApplicationDbContext db,
        SigningCredentials credentials,
        IPasswordHasher<User> hasher
    )
    {
        _db = db;
        _credentials = credentials;
        _hasher = hasher;
    }

    public class LoginCredentials
    {
        public required string username { get; set; }
        public required string password { get; set; }
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult login([FromBody] LoginCredentials loginCredentials)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == loginCredentials.username);
        if (user == null)
        {
            return NotFound($"User {loginCredentials.username} not found");
        }

        // should never happen, and this property is required in database, and so every query should return it
        if (user.PasswordHash == null)
        {
            return NotFound($"Couldn't find password hash for user {loginCredentials.username}");
        }
        var result = _hasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            loginCredentials.password
        );
        if (result != PasswordVerificationResult.Success)
        {
            return Unauthorized("invalid password");
        }

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

        var token = new JwtSecurityToken(
            /*claims not sctrictly necessary*/
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: _credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = tokenString });
    }
}
