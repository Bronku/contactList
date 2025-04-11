using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class MyApiController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok($"You requested item with ID: {id}\n");
    }
}
