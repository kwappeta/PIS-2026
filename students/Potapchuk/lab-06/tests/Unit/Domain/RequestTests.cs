public class RequestTests
{
    [Fact]
    public void CreateRequest_ShouldSetCorrectInitialState()
    {
        var request = Request.Create("Спасение туриста", "Описание", "user-123", "zone-45", 4);

        Assert.NotNull(request.Id);
        Assert.Equal("Спасение туриста", request.Title);
        Assert.Equal(RequestStatus.New, request.Status);
        Assert.Equal(4, request.EmergencyLevel);
        Assert.Single(request.GetEvents());
        Assert.IsType<RequestCreated>(request.GetEvents().First());
    }

    [Fact]
    public void AssignGroup_ShouldChangeGroupAndStatus()
    {
        var request = Request.Create("Test", null, "u1", "z1", 3);
        request.AssignGroup("group-10", "coord-1");

        Assert.Equal("group-10", request.GroupId);
        Assert.Equal(RequestStatus.Assigned, request.Status);
    }

    [Fact]
    public void Complete_ShouldSetCompletedStatus()
    {
        var request = Request.Create("Test", null, "u1", "z1", 3);
        request.Complete("user-5", "Успешно спасены");

        Assert.Equal(RequestStatus.Completed, request.Status);
        Assert.NotNull(request.CompletedAt);
    }
}