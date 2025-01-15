using GitHubUserActivity.Models;

namespace GitHubUserActivity.Services;

public interface IGitHubService
{
    public Task<IEnumerable<GitHubEvent>> GetUserEventsAsync(string username);
}
