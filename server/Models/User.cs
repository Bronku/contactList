using System.ComponentModel.DataAnnotations;

public enum ContactCategory
{
    Business,
    Personal,
    Other,
}

public class BusinessCategory
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }
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

    public int? BusinessCategoryId { get; set; }
    public BusinessCategory? BusinessCategory { get; set; }
    public string? CustomCategory { get; set; }

    [Required]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    [Phone]
    public required string PhoneNumber { get; set; }
}
