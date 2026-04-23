using Grpc.Core;
using RequestGrpc;
using System;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class RequestServiceImpl : RequestService.RequestServiceBase
    {
        public override Task<CreateRequestResponse> CreateRequest(CreateRequestRequest request, ServerCallContext context)
        {
            string requestId = "req-" + Guid.NewGuid().ToString()[..8];

            Console.WriteLine($"[gRPC Server] Создан запрос: {requestId} | {request.Title}");

            return Task.FromResult(new CreateRequestResponse
            {
                RequestId = requestId,
                Status = "Created"
            });
        }

        public override Task<RequestDto> GetRequest(GetRequestRequest request, ServerCallContext context)
        {
            return Task.FromResult(new RequestDto
            {
                Id = request.RequestId,
                Title = "Спасение туриста на горе",
                Status = "Active",
                EmergencyLevel = 4,
                ZoneId = "zone-45",
                RequesterId = "user-123"
            });
        }

        public override async Task StreamActiveRequests(StreamRequest request, 
            IServerStreamWriter<RequestDto> responseStream, 
            ServerCallContext context)
        {
            Console.WriteLine($"[gRPC Server] Начало стриминга для зоны: {request.ZoneId}");

            for (int i = 1; i <= 8; i++)
            {
                if (context.CancellationToken.IsCancellationRequested)
                    break;

                await responseStream.WriteAsync(new RequestDto
                {
                    Id = $"stream-{i}",
                    Title = $"Активная заявка #{i}",
                    Status = "Active",
                    EmergencyLevel = 3 + (i % 3),
                    ZoneId = request.ZoneId
                });

                await Task.Delay(800);
            }

            Console.WriteLine("[gRPC Server] Стриминг завершён.");
        }
    }
}