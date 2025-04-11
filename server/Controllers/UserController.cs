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

    [HttpPost]
    public IActionResult newUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("Invalid data.");
        }
        _userStore.addUser(user);
        return Ok($"Added User");
    }
}
