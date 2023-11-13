using Gw2AddonManagement.Core;
using Gw2AddonManagement.Data;
using Gw2AddonManagement.Exception;
using Gw2AddonManagement.Extensions;

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
        var result = await Get(assetUrl);
        var (downloadUrl, fileName) = result.GetContentAs<GitHubAssetResponse[]>()[0];

        return await Download(downloadUrl, location, fileName);
    }

    private async Task<string> Download(string downloadUrl, SaveLocation location, string name)
    {
        var result = await Get(downloadUrl);
        await using var stream = await result.Content.ReadAsStreamAsync();

        return _fileService.SaveToFile(stream, location, name);
    }

    public async Task<GitHubLatestReleaseResponse> GetLatestRelease(string owner, string repo)
    {
        var response = await Get($"{GetBaseUri(owner, repo)}/releases/latest");
        return response.GetContentAs<GitHubLatestReleaseResponse>();
    }

    private string GetBaseUri(string owner, string repo) => $"https://api.github.com/repos/{owner}/{repo}";

    private async Task<HttpResponseMessage> Get(string url)
    {
        var message = await _client.GetAsync(url);

        return message.StatusCode switch
        {
            HttpStatusCode.OK => message,
            _ => throw new GitHubRequestFailedException(message)
        };
    }
}