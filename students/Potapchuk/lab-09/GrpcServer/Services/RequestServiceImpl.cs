using Grpc.Core;
using RequestGrpc;

public class RequestServiceImpl : RequestService.RequestServiceBase
{
    public override Task<CreateRequestResponse> CreateRequest(CreateRequestRequest request, ServerCallContext context)
    {
        string requestId = "req-" + Guid.NewGuid().ToString()[..8];
        
        Console.WriteLine($"[gRPC] Created request: {requestId} - {request.Title}");

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

    // Server-side Streaming
    public override async Task StreamActiveRequests(StreamRequest request, 
        IServerStreamWriter<RequestDto> responseStream, 
        ServerCallContext context)
    {
        for (int i = 1; i <= 5; i++)
        {
            if (context.CancellationToken.IsCancellationRequested)
                break;

            await responseStream.WriteAsync(new RequestDto
            {
                Id = $"stream-req-{i}",
                Title = $"Активная заявка #{i}",
                Status = "Active",
                EmergencyLevel = 3
            });

            await Task.Delay(1000); // 1 секунда между сообщениями
        }
    }
}