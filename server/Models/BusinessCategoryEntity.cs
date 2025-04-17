using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class BusinessCategoryEntity
{
    public int Id { get; set; }
    [MaxLength(2048)] public required string Name { get; set; }
}