using Grpc.Net.Client;
using RequestGrpc;
using System;
using System.Threading.Tasks;
using Grpc.Core;
using System.Collections.Generic;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== gRPC Client Started ===\n");

            using var channel = GrpcChannel.ForAddress("http://localhost:5150");
            var client = new RequestService.RequestServiceClient(channel);

            // 1. Создание заявки
            Console.WriteLine("Отправляем запрос на создание заявки...");
            var createReply = await client.CreateRequestAsync(new CreateRequestRequest
            {
                Title = "Тестовая заявка через gRPC",
                Description = "Создано из клиента",
                RequesterId = "user-001",
                ZoneId = "zone-10",
                EmergencyLevel = 5
            });

            Console.WriteLine($"✅ Заявка создана! ID: {createReply.RequestId}, Статус: {createReply.Status}\n");

            // 2. Server-side Streaming
            Console.WriteLine("Запускаем стриминг активных заявок...");
            using var streamingCall = client.StreamActiveRequests(new StreamRequest { ZoneId = "zone-10" });

            await foreach (var response in streamingCall.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"→ Получена заявка: {response.Title} | Статус: {response.Status} | Уровень: {response.EmergencyLevel}");
            }

            Console.WriteLine("\n✅ Streaming завершён.");
            Console.ReadKey();
        }
    }
}