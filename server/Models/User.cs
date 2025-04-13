using System.ComponentModel.DataAnnotations;

public enum ContactCategory
{
    Business,
    Personal,
    Other,
}

public enum BusinessCategory
{
    Boss,
    Client,
    Manager,
    Developer,
    Hr,
}

public class User
{
    public int? Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Surname { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")]
    public required string Password { get; set; }

    [Required]
    public ContactCategory Category { get; set; }

    public BusinessCategory? BusinessCategory { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [Required]
    [Phone]
    public required string PhoneNumber { get; set; }
}
