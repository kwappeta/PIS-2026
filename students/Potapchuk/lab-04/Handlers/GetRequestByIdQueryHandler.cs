using Application.DTOs;
using Application.Interfaces;
using Application.Queries;

public class GetRequestByIdQueryHandler
{
    private readonly IRequestRepository _repository;

    public GetRequestByIdQueryHandler(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<RequestDto> HandleAsync(GetRequestByIdQuery query)
    {
        var request = await _repository.GetByIdAsync(query.RequestId)
            ?? throw new KeyNotFoundException($"Request with id {query.RequestId} not found");

        return RequestDto.FromDomain(request);
    }
}