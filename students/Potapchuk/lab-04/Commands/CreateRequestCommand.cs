using System.ComponentModel.DataAnnotations;

public record CreateRequestCommand
{
    [Required, MinLength(5), MaxLength(200)]
    public string Title { get; init; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; init; }

    [Required]
    public string RequesterId { get; init; } = string.Empty;

    [Required]
    public string InitialZoneId { get; init; } = string.Empty;

    [Range(1, 5)]
    public int EmergencyLevel { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}