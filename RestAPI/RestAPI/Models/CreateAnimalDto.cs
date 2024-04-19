using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models;

public class CreateAnimalDto()
{
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(200)]
    public string? Description { get; set; } = null;
    [Required, MaxLength(200)]
    public string Category { get; set; } = string.Empty;
    [Required, MaxLength(200)]
    public string Area { get; set; } = string.Empty;
}