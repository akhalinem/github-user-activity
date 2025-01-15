using GitHubUserActivity.Models;
using GitHubUserActivity.Services;

namespace GitHubUserActivity.Tests.Services;

public class GitHubEventFormatterTests
{
    private readonly GitHubEventFormatter _formatter = new();

    [Theory]
    [InlineData("PushEvent", "test/repo", 1, "Pushed 1 commit(s) to test/repo")]
    [InlineData("IssuesEvent", "test/repo", null, "opened an issue in test/repo")]
    [InlineData("WatchEvent", "test/repo", null, "Starred test/repo")]
    [InlineData("UnknownEvent", "test/repo", null, "UnknownEvent in test/repo")]
    public void Format_ReturnsCorrectString(string eventType, string repoName, int? commitCount, string expected)
    {
        var githubEvent = new GitHubEvent
        {
            Type = eventType,
            Repo = new GitHubRepo { Name = repoName },
            Payload = new GitHubPayload
            {
                Action = eventType == "WatchEvent" ? "started" : "opened",
                Commits = commitCount.HasValue ? [new GitHubCommit()] : null
            }
        };

        var result = _formatter.Format(githubEvent);

        Assert.Equal(expected, result);
    }
}
