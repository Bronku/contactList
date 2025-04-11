using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

public class SimpleIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SimpleIntegrationTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task RootApiUserRetursUsers()
    {
        var response = await _client.GetAsync("/api/User");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("[]", content);

        var user = new User { Name = "Jakub", Contact = "+48 508 442 510" };
        var postContent = JsonContent.Create(user);
        await _client.PostAsync("/api/User", postContent);
        response = await _client.GetAsync("/api/User");
        content = await response.Content.ReadAsStringAsync();
        Assert.Equal("[{\"name\":\"Jakub\",\"contact\":\"+48 508 442 510\"}]", content);
    }
}
