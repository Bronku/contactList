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
        var newContact = new User
        {
            Name = "Jan",
            Surname = "Kowalski",
            Email = "jan.test@example.com",
            Password = "P@ssw0rd123!",
            Category = ContactCategory.Business,
            BusinessCategoryId = 1,
            PhoneNumber = "+48 123 456 789",
            DateOfBirth = new DateTime(1980, 5, 15),
        };

        var response = await _client.PostAsJsonAsync("/api/User", newContact);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        Assert.True(response.Headers.TryGetValues("Location", out var locationHeaders));
        Assert.NotNull(locationHeaders);
        Assert.NotEmpty(locationHeaders);
        var locationUri = new Uri(locationHeaders.First());

        var getResponse = await _client.GetAsync(locationUri);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var createdContact = await getResponse.Content.ReadFromJsonAsync<User>();
        Assert.NotNull(createdContact);
        Assert.Equal(newContact.Email, createdContact.Email);
        Assert.Equal(newContact.BusinessCategoryId, createdContact.BusinessCategoryId);
        Assert.True(createdContact.Id >= 0);
    }
}
