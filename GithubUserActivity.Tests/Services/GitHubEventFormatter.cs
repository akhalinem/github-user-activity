using GithubUserActivity.Models;
using GithubUserActivity.Services;

namespace GithubUserActivity.Tests.Services;

public class GithubEventFormatterTests
{
    private readonly GithubEventFormatter _formatter = new();

    [Theory]
    [InlineData("PushEvent", "test/repo", 1, "Pushed 1 commit(s) to test/repo")]
    [InlineData("IssuesEvent", "test/repo", null, "opened an issue in test/repo")]
    [InlineData("WatchEvent", "test/repo", null, "Starred test/repo")]
    [InlineData("UnknownEvent", "test/repo", null, "UnknownEvent in test/repo")]
    public void Format_ReturnsCorrectString(string eventType, string repoName, int? commitCount, string expected)
    {
        var githubEvent = new GithubEvent
        {
            Type = eventType,
            Repo = new GithubRepo { Name = repoName },
            Payload = new GithubPayload
            {
                Action = eventType == "WatchEvent" ? "started" : "opened",
                Commits = commitCount.HasValue ? [new GithubCommit()] : null
            }
        };

        var result = _formatter.Format(githubEvent);

        Assert.Equal(expected, result);
    }
}
