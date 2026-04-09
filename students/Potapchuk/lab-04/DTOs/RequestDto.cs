using Domain.Entities;

public record RequestDto
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Status { get; init; } = string.Empty;
    public int EmergencyLevel { get; init; }
    public string ZoneId { get; init; } = string.Empty;
    public string? GroupId { get; init; }
    public string RequesterId { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? CompletedAt { get; init; }

    public static RequestDto FromDomain(Request request)
    {
        return new RequestDto
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status.ToString(),
            EmergencyLevel = request.EmergencyLevel,
            ZoneId = request.ZoneId,
            GroupId = request.GroupId,
            RequesterId = request.RequesterId,
            CreatedAt = request.CreatedAt,
            CompletedAt = request.CompletedAt
        };
    }
}