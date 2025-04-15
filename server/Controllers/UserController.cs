using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public UserController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult getUsers()
    {
        var users = _db.Users.OrderBy(u => u.Id).ToList();
        return Ok(users);
    }

    [HttpGet("{Id}")]
    public IActionResult getUser(int Id)
    {
        var user = _db.Users.FirstOrDefault(u => u.Id == Id);
        if (user == null)
        {
            return NotFound($"User {Id} not found");
        }
        return Ok(user);
    }

    [HttpPost]
    public IActionResult newUser([FromBody] User user)
    {
        _db.Add(user);
        _db.SaveChanges();
        return CreatedAtAction(nameof(getUser), new { Id = user.Id }, user);
    }

    [HttpPut]
    public IActionResult updateUser([FromBody] User user)
    {
        _db.Update(user);
        _db.SaveChanges();
        return CreatedAtAction(nameof(getUser), new { Id = user.Id }, user);
    }
}
