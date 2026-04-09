using System.ComponentModel.DataAnnotations;

public record ChangeRequestZoneCommand
{
    [Required]
    public string RequestId { get; init; } = string.Empty;

    [Required]
    public string NewZoneId { get; init; } = string.Empty;

    [Required]
    public string ChangedBy { get; init; } = string.Empty;
}