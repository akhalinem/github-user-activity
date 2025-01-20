# GitHub User Activity CLI

A command-line interface tool that displays recent GitHub activity for a specified user.

## Description

This CLI application fetches and displays the recent activity of a GitHub user using the GitHub API. It's built with C# and .NET, focusing on simplicity and reliability without external HTTP client dependencies.

## Features

- Fetch user's recent GitHub activity
- Display activities in a readable format
- Handle API errors gracefully
- No external dependencies for HTTP requests

## Requirements

- .NET 9.0
- GitHub API access (no authentication required for basic usage)

## Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/github-user-activity.git
cd github-user-activity
```

2. Build the project:
```bash
dotnet build
```

## Usage

Run the application with a GitHub username as an argument:

```bash
github-activity <username>
```

Example:
```bash
github-activity kamranahmedse
```

### Output Format

The tool displays activities in an easy-to-read format:
```
- Pushed 3 commits to kamranahmedse/developer-roadmap
- Opened a new issue in kamranahmedse/developer-roadmap
- Starred kamranahmedse/developer-roadmap
```

### Error Handling

The application handles various error scenarios:
- Invalid username
- GitHub API rate limiting
- Network connectivity issues
- API response errors

## Technical Details

- Uses GitHub Events API: `https://api.github.com/users/<username>/events`
- Built with native HttpClient for API requests
- JSON deserialization using System.Text.Json