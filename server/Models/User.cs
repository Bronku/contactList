using System.ComponentModel.DataAnnotations;

namespace server.Models;

// using .NET Core Identity for keeping track of users seems a bit overkill for such a simple app
public class User
{
    [Key] public int Id { get; init; }

    [Required] [MaxLength(2048)] public required string Username { get; init; }

    [Required] [MaxLength(2048)] public string? PasswordHash { get; set; }
}