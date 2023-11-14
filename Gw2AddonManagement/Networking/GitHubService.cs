using System.IO.Compression;
using Gw2AddonManagement.Core;
using Gw2AddonManagement.Data;
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

    public async Task<string[]> Download(string assetUrl, SaveLocation location)
    {
        var result = await _client.Get(assetUrl);
        var assetResponses = result.GetContentAs<GitHubAssetResponse[]>();
        var files = new List<string>();

        foreach (var assetResponse in assetResponses)
        {
            var extension = Path.GetExtension(assetResponse.FileName);

            switch (extension)
            {
                case ".dll":
                {
                    files.Add(await Download(assetResponse.DownloadUrl, location, assetResponse.FileName));
                    break;
                }
                case ".zip":
                {
                    files.AddRange(await DownloadZip(assetResponse.DownloadUrl, location));
                    break;
                }
            }
        }

        return files.ToArray();
    }

    private async Task<string[]> DownloadZip(string downloadUrl, SaveLocation location)
    {
        var result = await _client.Get(downloadUrl);
        await using var stream = await result.Content.ReadAsStreamAsync();
        var archive = new ZipArchive(stream, ZipArchiveMode.Read);
        var files = new List<string>();

        foreach (var entry in archive.Entries)
        {
            if (Path.GetExtension(entry.Name) is ".dll")
            {
                var open = entry.Open();
                var file = _fileService.SaveToFile(open, location, entry.Name);
                files.Add(file);
            }
        }

        return files.ToArray();
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