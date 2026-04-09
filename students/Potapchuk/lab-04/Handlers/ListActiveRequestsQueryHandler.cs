using Application.DTOs;
using Application.Interfaces;
using Application.Queries;

public class ListActiveRequestsQueryHandler
{
    private readonly IRequestRepository _repository;

    public ListActiveRequestsQueryHandler(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RequestDto>> HandleAsync(ListActiveRequestsQuery query)
    {
        var requests = await _repository.GetAllActiveAsync(query.ZoneId);

        return requests
            .Where(r => query.MinEmergencyLevel == null || r.EmergencyLevel >= query.MinEmergencyLevel)
            .Select(RequestDto.FromDomain)
            .ToList();
    }
}