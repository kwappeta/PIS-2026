using System.ComponentModel.DataAnnotations;

public record CompleteRequestCommand
{
    [Required]
    public string RequestId { get; init; } = string.Empty;

    [Required]
    public string CompletedBy { get; init; } = string.Empty;

    [Required, MinLength(10)]
    public string ResultSummary { get; init; } = string.Empty;

    public DateTime CompletedAt { get; init; } = DateTime.UtcNow;
}