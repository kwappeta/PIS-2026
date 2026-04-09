using Application.Commands;
using Application.Interfaces;
using Domain.Entities;

public class ChangeRequestZoneCommandHandler
{
    private readonly IRequestRepository _repository;
    private readonly IEventPublisher _publisher;

    public ChangeRequestZoneCommandHandler(IRequestRepository repository, IEventPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task HandleAsync(ChangeRequestZoneCommand command)
    {
        var request = await _repository.GetByIdAsync(command.RequestId)
            ?? throw new KeyNotFoundException($"Request with id {command.RequestId} not found");

        request.ChangeZone(command.NewZoneId, command.ChangedBy);

        await _repository.SaveAsync(request);

        foreach (var domainEvent in request.GetEvents())
        {
            await _publisher.PublishAsync(domainEvent);
        }
    }
}
