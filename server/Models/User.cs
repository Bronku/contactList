using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class User
{
    [Key]
    public int Id { get; init; }

    [Required]
    [MaxLength(2048)]
    public required string Username { get; init; }

    [Required]
    [MaxLength(2048)]
    public string? PasswordHash { get; set; }
}