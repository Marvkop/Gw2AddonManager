namespace Gw2AddonManagement.Data;

public record GitHubAssetResponse(
    [JsonProperty("browser_download_url")] string DownloadUrl,
    [JsonProperty("name")] string FileName
);