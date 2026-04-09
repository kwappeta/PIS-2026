using Application.Commands.Handlers;
using Application.Handlers;                    // Query Handlers
using Application.Interfaces;
using Application.Services;
using Domain.Events;
using Infrastructure.Repositories;            // если у тебя уже есть реализация
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ==================== Регистрация сервисов ====================

// 1. Application Layer - Handlers
builder.Services.AddScoped<CreateRequestCommandHandler>();
builder.Services.AddScoped<AssignGroupToRequestCommandHandler>();
builder.Services.AddScoped<ActivateRequestCommandHandler>();
builder.Services.AddScoped<ChangeRequestZoneCommandHandler>();
builder.Services.AddScoped<CompleteRequestCommandHandler>();

builder.Services.AddScoped<GetRequestByIdQueryHandler>();
builder.Services.AddScoped<ListActiveRequestsQueryHandler>();

// 2. Application Service (Фасад)
builder.Services.AddScoped<IRequestService, RequestService>();

// 3. Infrastructure (реализации портов)
builder.Services.AddScoped<IRequestRepository, RequestRepository>();           // твоя реализация
builder.Services.AddScoped<IEventPublisher, EventPublisher>();                 // твоя реализация

// ==================== (Опционально) ====================

// Если в будущем будешь добавлять MediatR — можно использовать такой подход:
// builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateRequestCommand).Assembly));

var app = builder.Build();

// ==================== Middleware ====================

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();   // если будешь добавлять API контроллеры позже

app.Run();