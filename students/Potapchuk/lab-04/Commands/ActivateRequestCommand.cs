using System.ComponentModel.DataAnnotations;

public record ActivateRequestCommand
{
    [Required]
    public string RequestId { get; init; } = string.Empty;

    [Required]
    public string ActivatedBy { get; init; } = string.Empty;
}