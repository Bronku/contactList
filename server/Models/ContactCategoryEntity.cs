using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class ContactCategoryEntity
{
    public int Id { get; init; }

    [MaxLength(2048)] public required string Name { get; init; }
}