using GitHubUserActivity.Services;

namespace GitHubUserActivity;

public class Program
{
    private static readonly IGitHubService _githubService = new GitHubService();
    private static readonly GitHubEventFormatter _formatter = new();

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
                Console.WriteLine($"- {_formatter.Format(e)}");
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
}
