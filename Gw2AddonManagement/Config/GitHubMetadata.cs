namespace Gw2AddonManagement.Config;

public record GitHubMetadata(
    [JsonProperty("repo")] string Repo,
    [JsonProperty("owner")] string Owner
);