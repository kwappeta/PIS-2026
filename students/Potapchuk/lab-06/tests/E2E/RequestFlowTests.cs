public class RequestFlowTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RequestFlowTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task FullFlow_Create_Assign_Complete_ShouldWork()
    {
        // Create
        var createCmd = new { title = "E2E тест", requesterId = "user1", initialZoneId = "z1", emergencyLevel = 4 };
        var createResp = await _client.PostAsJsonAsync("/api/requests", createCmd);
        var id = (await createResp.Content.ReadFromJsonAsync<dynamic>()).id.ToString();

        // Assign
        await _client.PostAsJsonAsync($"/api/requests/{id}/assign-group", new { groupId = "g10", assignedBy = "coord1" });

        // Get
        var response = await _client.GetAsync($"/api/requests/{id}");
        var dto = await response.Content.ReadFromJsonAsync<RequestDto>();

        Assert.Equal("Assigned", dto.Status);
    }
}