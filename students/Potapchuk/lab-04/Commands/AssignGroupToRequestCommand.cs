using System.ComponentModel.DataAnnotations;

public record AssignGroupToRequestCommand
{
    [Required]
    public string RequestId { get; init; } = string.Empty;

    [Required]
    public string GroupId { get; init; } = string.Empty;

    [Required]
    public string AssignedBy { get; init; } = string.Empty;
}
