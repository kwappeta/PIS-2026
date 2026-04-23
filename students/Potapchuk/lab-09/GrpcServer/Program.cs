using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GrpcServer.Services;        // важно!

var builder = WebApplication.CreateBuilder(args);

// Добавляем gRPC сервисы
builder.Services.AddGrpc();

var app = builder.Build();

// Подключаем наш gRPC сервис
app.MapGrpcService<RequestServiceImpl>();

// Простая страница при открытии в браузере
app.MapGet("/", () => "✅ gRPC Server is running!\nUse a gRPC client to call services.");

app.Run();