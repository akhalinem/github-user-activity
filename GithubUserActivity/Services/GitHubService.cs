using System.Text.Json;
using GithubUserActivity.Models;

namespace GithubUserActivity.Services;

public class GithubService : IGithubService, IDisposable
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://api.github.com";
    private readonly bool _disposeClient;

    public GithubService(HttpClient? httpClient = null)
    {
        if (httpClient != null)
        {
            _httpClient = httpClient;
            _disposeClient = false;
        }
        else
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Github-User-Activity-CLI");
            _disposeClient = true;
        }
    }

    public async Task<IEnumerable<GithubEvent>> GetUserEventsAsync(string username)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/users/{username}/events");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GithubEvent>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            }) ?? [];
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException($"Failed to fetch Github events: {ex.Message}", ex);
        }
    }

    public void Dispose()
    {
        if (_disposeClient)
        {
            _httpClient.Dispose();
        }
    }
}
