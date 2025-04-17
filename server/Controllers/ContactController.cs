using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController(ApplicationDbContext db) : ControllerBase
{
    // GET /api/Contact returns a list of all contacts
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetContacts()
    {
        var users = await db.Contacts.ToListAsync();
        return Ok(users);
    }

    // GET /api/Contact/{int} returns one contact for the corresponding id 
    // not used in the client application
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetContact(int id)
    {
        // FirstOrDefault() instead of First to return a nice message
        // First() would trigger an exception, which would cause the server to respond code 500
        var contact = await db.Contacts.FirstOrDefaultAsync(u => u.Id == id);
        if (contact == null) return NotFound($"Contact {id} not found");
        return Ok(contact);
    }

    // POST /api/Contact saves a new element to the database
    // responds with an error when the form data is invalid
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> NewContact([FromBody] Contact contact)
    {
        if (contact.Id != 0) return BadRequest();
        db.Add(contact);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetContact), new { contact.Id }, contact);
    }

    // PUT /api/Contact updates an existing record 
    // responds with an error when form data is invalid, or the element doesn't exist
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateContact([FromBody] Contact contact)
    {
        db.Update(contact);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetContact), new { contact.Id }, contact);
    }

    // DELETE /api/Contact/{int} deletes the contact from database
    // responds with NotFound when there is no such contact, no news is good news
    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteContact(int id)
    {
        var contact = await db.Contacts.FirstOrDefaultAsync(u => u.Id == id);
        if (contact == null) return NotFound($"Contact {id} not found");
        db.Remove(contact);
        await db.SaveChangesAsync();
        return NoContent();
    }
}