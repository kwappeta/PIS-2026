public class InMemoryEventPublisher : IEventPublisher
{
    public Task PublishAsync(DomainEvent domainEvent)
    {
        Console.WriteLine($"[EVENT] {domainEvent.GetType().Name} published: {domainEvent}");
        // Здесь можно добавить RabbitMQ / Kafka позже
        return Task.CompletedTask;
    }
}