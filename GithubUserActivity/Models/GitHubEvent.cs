namespace GitHubUserActivity.Models;

public class GitHubEvent
{

    public string Type { get; set; } = string.Empty;
    public GitHubActor Actor { get; set; } = new();
    public GitHubRepo Repo { get; set; } = new();
    public GitHubPayload Payload { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class GitHubActor
{
    public string Login { get; set; } = string.Empty;
    public string DisplayLogin { get; set; } = string.Empty;
}

public class GitHubRepo
{
    public string Name { get; set; } = string.Empty;
}

public class GitHubPayload
{
    public string? Action { get; set; }
    public string? Ref { get; set; }
    public string? RefType { get; set; }
    public List<GitHubCommit>? Commits { get; set; }
}

public class GitHubCommit
{
    public string Sha { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
