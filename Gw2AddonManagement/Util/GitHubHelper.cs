namespace Gw2AddonManagement.Util;

public static class GitHubHelper
{
    public static string GetBaseUri(string owner, string repo) => $"https://api.github.com/repos/{owner}/{repo}";
}