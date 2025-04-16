using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController(ApplicationDbContext db) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetContacts()
    {
        var users = db.Contacts.OrderBy(u => u.Id).ToList();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public IActionResult GetContact(int id)
    {
        var contact = db.Contacts.FirstOrDefault(u => u.Id == id);
        if (contact == null) return NotFound($"Contact {id} not found");
        return Ok(contact);
    }

    [HttpPost]
    [Authorize]
    public IActionResult NewContact([FromBody] Contact contact)
    {
        db.Add(contact);
        db.SaveChanges();
        return CreatedAtAction(nameof(GetContact), new { contact.Id }, contact);
    }

    [HttpPut]
    [Authorize]
    public IActionResult UpdateContact([FromBody] Contact contact)
    {
        db.Update(contact);
        db.SaveChanges();
        return CreatedAtAction(nameof(GetContact), new { contact.Id }, contact);
    }
}