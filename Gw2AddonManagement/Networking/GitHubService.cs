using Gw2AddonManagement.Core;
using Gw2AddonManagement.Data;
using Gw2AddonManagement.Exception;
using Gw2AddonManagement.Extensions;
using Gw2AddonManagement.Util;

namespace Gw2AddonManagement.Networking;

public class GitHubService
{
    private readonly FileService _fileService;
    private readonly HttpClient _client = new();

    public GitHubService()
    {
        Ioc.Default.InitService(out _fileService);
        _client.DefaultRequestHeaders.Add("User-Agent", "Gw2AddonManagement by Marvkop");
    }

    public async Task<string> Download(string assetUrl, SaveLocation location)
    {
        var result = await _client.Get(assetUrl);
        var (downloadUrl, fileName) = result.GetContentAs<GitHubAssetResponse[]>()[0];

        return await Download(downloadUrl, location, fileName);
    }

    private async Task<string> Download(string downloadUrl, SaveLocation location, string name)
    {
        var result = await _client.Get(downloadUrl);
        await using var stream = await result.Content.ReadAsStreamAsync();

        return _fileService.SaveToFile(stream, location, name);
    }

    public async Task<GitHubLatestReleaseResponse> GetLatestRelease(string owner, string repo)
    {
        var response = await _client.Get($"{GitHubHelper.GetBaseUri(owner, repo)}/releases/latest");
        return response.GetContentAs<GitHubLatestReleaseResponse>();
    }
}