var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Infrastructure
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IEventPublisher, InMemoryEventPublisher>();

// Application (из lab-04)
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<CreateRequestCommandHandler>();
// ... добавь все остальные handlers

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();