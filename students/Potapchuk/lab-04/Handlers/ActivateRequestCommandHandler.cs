using Application.Commands;
using Application.Interfaces;
using Domain.Entities;

public class ActivateRequestCommandHandler
{
    private readonly IRequestRepository _repository;
    private readonly IEventPublisher _publisher;

    public ActivateRequestCommandHandler(IRequestRepository repository, IEventPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task HandleAsync(ActivateRequestCommand command)
    {
        var request = await _repository.GetByIdAsync(command.RequestId)
            ?? throw new KeyNotFoundException($"Request with id {command.RequestId} not found");

        request.Activate(command.ActivatedBy);

        await _repository.SaveAsync(request);

        foreach (var domainEvent in request.GetEvents())
        {
            await _publisher.PublishAsync(domainEvent);
        }
    }
}