using System.ComponentModel.DataAnnotations;

namespace server.Models;

// MaxLength of 2048 seems reasonable for any contact information string
// setting it in advance could potentially make the application faster
public class Contact
{
    // autoincremented primary key by default
    public int Id { get; init; }
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

    [Required] public required int ContactCategoryId { get; init; }

    public int? BusinessCategoryId { get; init; }

    [MaxLength(2048)] public string? OtherCategory { get; init; }

    public DateTime? DateOfBirth { get; init; }

    [Required] [Phone] [MaxLength(2048)] public required string PhoneNumber { get; init; }
}