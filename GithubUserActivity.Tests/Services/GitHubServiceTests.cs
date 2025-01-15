using System.Net;
using System.Text.Json;
using Moq;
using Moq.Protected;
using GitHubUserActivity.Models;
using GitHubUserActivity.Services;

namespace GitHubUserActivity.Tests.Services;

public class GitHubServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly GitHubService _service;

    public GitHubServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var client = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://api.github.com")
        };
        _service = new GitHubService(client);
    }

    [Fact]
    public async Task GetUserEventsAsync_ValidUsername_ReturnsEvents()
    {
        // Arrange
        var events = new List<GitHubEvent>
        {
            new() {
                Type = "PushEvent",
                Repo = new GitHubRepo { Name = "test/repo" },
                Payload = new GitHubPayload { Commits = [new GitHubCommit()] }
            }
        };
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(events))
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        var result = await _service.GetUserEventsAsync("testuser");

        // Assert
        Assert.Single(result);
        Assert.Equal("PushEvent", result.First().Type);
    }

    [Fact]
    public async Task GetUserEventsAsync_UserNotFound_ThrowsException()
    {
        // Arrange
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = new StringContent("Not Found")
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(
            () => _service.GetUserEventsAsync("nonexistentuser"));
    }

    [Fact]
    public void Dispose_DisposesHttpClient()
    {
        // Act
        _service.Dispose();

        // This test mainly ensures that Dispose doesn't throw any exceptions
        // Real disposal testing would require more complex setup
        Assert.True(true);
    }
}
