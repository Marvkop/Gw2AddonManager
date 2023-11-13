using Gw2AddonManagement.Extensions;
using Gw2AddonManagement.Networking;

namespace Gw2AddonManagement.Core.Updater;

public class GitHubUpdater : IAddonUpdater
{
    private readonly GitHubService _gitHubService;
    private readonly SaveLocation _saveLocation;
    private readonly string _addonName;
    private readonly string _repoOwner;
    private readonly string _repoName;
    private string? _currentVersion;
    private string? _assetUrl;
    private string? _nextVersion;

    public GitHubUpdater(
        SaveLocation saveLocation,
        string addonName,
        string repoOwner,
        string repoName,
        string? currentVersion)
    {
        Ioc.Default.InitService(out _gitHubService);
        _currentVersion = currentVersion;
        _saveLocation = saveLocation;
        _addonName = addonName;
        _repoOwner = repoOwner;
        _repoName = repoName;
    }

    public SaveLocation GetSaveLocation() => _saveLocation;

    public string GetAddonName() => _addonName;

    public string GetCurrentVersion() => _currentVersion ?? "";

    public string GetNextVersion() => _nextVersion ?? "";

    public void NotifyDelete() => _currentVersion = null;

    public async Task<string> Download()
    {
        if (_assetUrl == null) throw new ArgumentNullException(nameof(_assetUrl));
        if (_nextVersion == null) throw new ArgumentNullException(nameof(_nextVersion));

        _currentVersion = _nextVersion ?? throw new ArgumentNullException(nameof(_nextVersion));

        return await _gitHubService.Download(_assetUrl, GetSaveLocation());
    }

    public async Task<bool> NeedsUpdate()
    {
        if (_assetUrl == null)
        {
            try
            {
                (_assetUrl, _nextVersion, _) = await _gitHubService.GetLatestRelease(_repoOwner, _repoName);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return _nextVersion != _currentVersion;
    }
}