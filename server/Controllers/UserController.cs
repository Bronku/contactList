using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private UserStore _userStore;

    public UserController(UserStore userStore)
    {
        _userStore = userStore;
    }

    [HttpGet]
    public IActionResult getUsers()
    {
        return Ok(_userStore.getUsers());
    }

    [HttpGet("{Id}")]
    public IActionResult getUser(int Id)
    {
        try
        {
            return Ok(_userStore.getUserById(Id));
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult newUser([FromBody] User user)
    {
        _userStore.addUser(user);
        return CreatedAtAction(nameof(getUser), new { id = user.Id }, User);
    }
}
