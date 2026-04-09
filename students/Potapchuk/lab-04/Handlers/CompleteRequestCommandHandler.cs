using Application.Commands;
using Application.Interfaces;
using Domain.Entities;

public class CompleteRequestCommandHandler
{
    private readonly IRequestRepository _repository;
    private readonly IEventPublisher _publisher;

    public CompleteRequestCommandHandler(IRequestRepository repository, IEventPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task HandleAsync(CompleteRequestCommand command)
    {
        var request = await _repository.GetByIdAsync(command.RequestId)
            ?? throw new KeyNotFoundException($"Request with id {command.RequestId} not found");

        request.Complete(command.CompletedBy, command.ResultSummary);

        await _repository.SaveAsync(request);

        foreach (var domainEvent in request.GetEvents())
        {
            await _publisher.PublishAsync(domainEvent);
        }
    }
}