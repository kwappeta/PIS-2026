public class CreateRequestCommandHandler
{
    private readonly IRequestRepository _repository;
    private readonly IEventPublisher _publisher;

    public CreateRequestCommandHandler(IRequestRepository repository, IEventPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<string> HandleAsync(CreateRequestCommand command)
    {
        // Валидация на уровне Application (примитивы)
        if (string.IsNullOrWhiteSpace(command.Title))
            throw new ArgumentException("Title is required", nameof(command.Title));

        var request = Request.Create(
            title: command.Title,
            description: command.Description,
            requesterId: command.RequesterId,
            zoneId: command.InitialZoneId,
            emergencyLevel: command.EmergencyLevel);

        await _repository.SaveAsync(request);

        // Публикация событий
        foreach (var domainEvent in request.GetEvents())
        {
            await _publisher.PublishAsync(domainEvent);
        }

        return request.Id;
    }
}