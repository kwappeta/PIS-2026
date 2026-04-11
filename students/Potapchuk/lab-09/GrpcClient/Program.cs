using Grpc.Net.Client;
using RequestGrpc;

var channel = GrpcChannel.ForAddress("http://localhost:5001");
var client = new RequestService.RequestServiceClient(channel);

Console.WriteLine("=== gRPC Client ===");

// Unary call - Create
var createReply = await client.CreateRequestAsync(new CreateRequestRequest
{
    Title = "Тестовая заявка через gRPC",
    RequesterId = "user-001",
    ZoneId = "zone-10",
    EmergencyLevel = 5
});
Console.WriteLine($"Создан запрос: {createReply.RequestId} | Статус: {createReply.Status}");

// Server Streaming
Console.WriteLine("\n=== Streaming активных заявок ===");
using var streamingCall = client.StreamActiveRequests(new StreamRequest { ZoneId = "zone-10" });

await foreach (var response in streamingCall.ResponseStream.ReadAllAsync())
{
    Console.WriteLine($"→ Получена заявка: {response.Title} ({response.Status})");
}

Console.WriteLine("Streaming завершён.");