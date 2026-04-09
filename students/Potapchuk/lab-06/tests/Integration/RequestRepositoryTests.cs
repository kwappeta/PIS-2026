public class RequestRepositoryTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16")
        .Build();

    private AppDbContext _context = null!;
    private RequestRepository _repository = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(_dbContainer.GetConnectionString())
            .Options;

        _context = new AppDbContext(options);
        await _context.Database.MigrateAsync();

        _repository = new RequestRepository(_context);
    }

    public Task DisposeAsync() => _dbContainer.DisposeAsync().AsTask();

    [Fact]
    public async Task SaveAndGet_ShouldWorkCorrectly()
    {
        var request = Request.Create("Интеграционный тест", null, "user1", "zone1", 5);
        await _repository.SaveAsync(request);

        var found = await _repository.GetByIdAsync(request.Id);

        Assert.NotNull(found);
        Assert.Equal(request.Title, found.Title);
    }
}