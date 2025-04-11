using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
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
        IReadOnlyList<User> users = _userStore.getUsers();
        return Ok(users);
    }

    [HttpPost]
    public IActionResult newUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("Invalid data.");
        }
        _userStore.addUser(user);

        foreach (var u in _userStore.getUsers())
        {
            Console.WriteLine($"  Name: {u.Name}");
        }
        return Ok($"Added User");
    }
}
