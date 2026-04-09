public class CreateRequestHandlerTests
{
    private readonly Mock<IRequestRepository> _repoMock = new();
    private readonly Mock<IEventPublisher> _publisherMock = new();
    private readonly CreateRequestCommandHandler _handler;

    public CreateRequestHandlerTests()
    {
        _handler = new CreateRequestCommandHandler(_repoMock.Object, _publisherMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldSaveRequest_AndPublishEvent()
    {
        var command = new CreateRequestCommand 
        { 
            Title = "Тестовая заявка", 
            RequesterId = "user-1", 
            InitialZoneId = "zone-1", 
            EmergencyLevel = 3 
        };

        var id = await _handler.HandleAsync(command);

        Assert.NotNull(id);
        _repoMock.Verify(r => r.SaveAsync(It.IsAny<Request>()), Times.Once);
        _publisherMock.Verify(p => p.PublishAsync(It.IsAny<DomainEvent>()), Times.AtLeastOnce);
    }
}