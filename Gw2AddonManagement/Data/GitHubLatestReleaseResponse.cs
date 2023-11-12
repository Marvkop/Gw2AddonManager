using Newtonsoft.Json;

namespace Gw2AddonManagement.Data;

public record GitHubLatestReleaseResponse(
    [JsonProperty("assets_url")] string AssetsUrl,
    [JsonProperty("name")] string Name
);