using System.Text.Json;
using GitHubUserActivity.Models;

namespace GitHubUserActivity.Services;

public class GitHubService : IDisposable
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://api.github.com";

    public GitHubService()
    {
        _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHub-User-Activity-CLI");
    }

    public async Task<List<GitHubEvent>> GetUserEventsAsync(string username)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/users/{username}/events");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GitHubEvent>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            }) ?? [];
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to fetch GitHub events: {ex.Message}", ex);
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
