using Application.Commands;
using Application.Interfaces;
using Domain.Entities;

public class AssignGroupToRequestCommandHandler
{
    private readonly IRequestRepository _repository;
    private readonly IEventPublisher _publisher;

    public AssignGroupToRequestCommandHandler(IRequestRepository repository, IEventPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task HandleAsync(AssignGroupToRequestCommand command)
    {
        var request = await _repository.GetByIdAsync(command.RequestId)
            ?? throw new KeyNotFoundException($"Request with id {command.RequestId} not found");

        request.AssignGroup(command.GroupId, command.AssignedBy);

        await _repository.SaveAsync(request);

        foreach (var domainEvent in request.GetEvents())
        {
            await _publisher.PublishAsync(domainEvent);
        }
    }
}