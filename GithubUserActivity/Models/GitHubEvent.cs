namespace GithubUserActivity.Models;

public class GithubEvent
{

    public string Type { get; set; } = string.Empty;
    public GithubActor Actor { get; set; } = new();
    public GithubRepo Repo { get; set; } = new();
    public GithubPayload Payload { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class GithubActor
{
    public string Login { get; set; } = string.Empty;
    public string DisplayLogin { get; set; } = string.Empty;
}

public class GithubRepo
{
    public string Name { get; set; } = string.Empty;
}

public class GithubPayload
{
    public string? Action { get; set; }
    public string? Ref { get; set; }
    public string? RefType { get; set; }
    public List<GithubCommit>? Commits { get; set; }
}

public class GithubCommit
{
    public string Sha { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
