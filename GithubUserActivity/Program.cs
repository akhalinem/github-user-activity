using GitHubUserActivity.Models;
using GitHubUserActivity.Services;

namespace GitHubUserActivity;

public class Program
{
    private static readonly GitHubService _githubService = new();

    public static async Task<int> Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a GitHub username");
            Console.WriteLine("Usage: github-activity <username>");
            return 1;
        }

        var username = args[0];

        try
        {
            var events = await _githubService.GetUserEventsAsync(username);

            foreach (var e in events)
            {
                Console.WriteLine($"- {DisplayEvent(e)}");
            }

            return 0;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error: Unable to fetch events for user '{username}'");
            Console.WriteLine($"Details: {ex.Message}");
            return 1;
        }
    }

    private static string DisplayEvent(GitHubEvent e)
    {
        return e.Type switch
        {
            "PushEvent" => $"Pushed {e.Payload.Commits?.Count} commit(s) to {e.Repo.Name}",
            "IssuesEvent" => $"{e.Payload.Action} an issue in {e.Repo.Name}",
            "WatchEvent" when e.Payload.Action == "started" => $"Starred {e.Repo.Name}",
            "CreateEvent" when e.Payload.RefType == "repository" => $"Created {e.Repo.Name} repository",
            "CreateEvent" => $"Created {e.Payload.Ref} branch in {e.Repo.Name}",
            "ForkEvent" => $"Forked {e.Repo.Name}",
            "PublicEvent" => $"Made {e.Repo.Name} public",
            _ => $"{e.Type} in {e.Repo.Name}"
        };
    }
}
