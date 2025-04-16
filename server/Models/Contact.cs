using System.ComponentModel.DataAnnotations;

namespace server.Models;

public enum ContactCategory
{
    Business,
    Personal,
    Other
}

public enum BusinessCategory
{
    Boss,
    Client,
    Manager,
    Developer,
    Hr
}

public class Contact
{
    public int? Id { get; init; }

    [Required] [MaxLength(2048)] public required string Name { get; init; }

    [Required] [MaxLength(2048)] public required string Surname { get; init; }

    [Required]
    [EmailAddress]
    [MaxLength(2048)]
    public required string Email { get; init; }

    [Required]
    [MinLength(8)]
    [MaxLength(2048)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")]
    public required string Password { get; init; }

    [Required] public ContactCategory Category { get; init; }

    public BusinessCategory? BusinessCategory { get; init; }

    public DateTime? DateOfBirth { get; init; }

    [Required] [Phone] [MaxLength(2048)] public required string PhoneNumber { get; init; }
}