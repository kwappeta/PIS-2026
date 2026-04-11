public class RabbitMqPublisher
{
    public void Publish(string eventName, object data)
    {
        Console.WriteLine($"[RabbitMQ] Event published: {eventName} -> {JsonSerializer.Serialize(data)}");
        // Реальная отправка в RabbitMQ позже
    }
}