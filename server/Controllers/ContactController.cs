using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public ContactController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult getContacts()
    {
        var users = _db.Contacts.OrderBy(u => u.Id).ToList();
        return Ok(users);
    }

    [HttpGet("{Id}")]
    public IActionResult getContact(int Id)
    {
        var contact = _db.Contacts.FirstOrDefault(u => u.Id == Id);
        if (contact == null)
        {
            return NotFound($"Contact {Id} not found");
        }
        return Ok(contact);
    }

    [HttpPost]
    public IActionResult newContact([FromBody] Contact contact)
    {
        _db.Add(contact);
        _db.SaveChanges();
        return CreatedAtAction(nameof(getContact), new { Id = contact.Id }, contact);
    }

    [HttpPut]
    public IActionResult updateContact([FromBody] Contact contact)
    {
        _db.Update(contact);
        _db.SaveChanges();
        return CreatedAtAction(nameof(getContact), new { Id = contact.Id }, contact);
    }
}
