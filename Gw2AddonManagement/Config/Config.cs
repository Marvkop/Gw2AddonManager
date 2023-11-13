namespace Gw2AddonManagement.Config;

public record Config(
    [JsonProperty("basePath")] string? BasePath,
    [JsonProperty("addons")] Dictionary<string, Addon> Addons
);