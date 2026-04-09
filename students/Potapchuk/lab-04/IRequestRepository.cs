using Domain.Entities;

public interface IRequestRepository
{
    Task SaveAsync(Request request);
    Task<Request?> GetByIdAsync(string requestId);
    Task<List<Request>> GetAllActiveAsync(string? zoneId = null);
}