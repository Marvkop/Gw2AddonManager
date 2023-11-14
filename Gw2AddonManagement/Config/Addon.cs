using Gw2AddonManagement.Core;

namespace Gw2AddonManagement.Config;

public record Addon(
    [JsonProperty("name")] string Name,
    [JsonProperty("type")] string Type,
    [property: Obsolete]
    [JsonProperty("file")]
    string? File,
    [JsonProperty("files")] string[]? Files,
    [JsonProperty("version")] string? Version,
    [JsonProperty("location")] SaveLocation Location,
    [JsonProperty("metadata")] Dictionary<string, string> Metadata
);