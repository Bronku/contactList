using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
public class PublicController : ControllerBase
{
    [HttpGet]
    public IActionResult Root()
    {
        return Ok("Hello root\n");
    }
}
