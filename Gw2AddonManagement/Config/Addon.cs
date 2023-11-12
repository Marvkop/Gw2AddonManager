using System.Collections.Generic;
using Gw2AddonManagement.Core;
using Newtonsoft.Json;

namespace Gw2AddonManagement.Config;

public record Addon(
    [JsonProperty("name")] string Name,
    [JsonProperty("type")] string Type,
    [JsonProperty("file")] string? File,
    [JsonProperty("version")] string? Version,
    [JsonProperty("location")] SaveLocation Location,
    [JsonProperty("metadata")] Dictionary<string, string> Metadata
);