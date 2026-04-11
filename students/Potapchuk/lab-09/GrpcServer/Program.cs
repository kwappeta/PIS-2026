using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<RequestServiceImpl>();

app.MapGet("/", () => "gRPC Server is running. Use a gRPC client to call it.");

app.Run();