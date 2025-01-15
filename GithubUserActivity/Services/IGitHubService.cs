using GithubUserActivity.Models;

namespace GithubUserActivity.Services;

public interface IGithubService
{
    public Task<IEnumerable<GithubEvent>> GetUserEventsAsync(string username);
}
