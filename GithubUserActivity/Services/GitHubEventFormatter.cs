using GithubUserActivity.Models;

namespace GithubUserActivity.Services;

public class GithubEventFormatter
{
    public string Format(GithubEvent e)
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
