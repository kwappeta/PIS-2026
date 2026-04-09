public record ListActiveRequestsQuery
{
    public string? ZoneId { get; init; }
    public int? MinEmergencyLevel { get; init; }
}