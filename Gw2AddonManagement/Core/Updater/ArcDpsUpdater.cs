using Gw2AddonManagement.Extensions;
using Gw2AddonManagement.Networking;

namespace Gw2AddonManagement.Core.Updater;

public class ArcDpsUpdater : IAddonUpdater
{
    private readonly ArcDpsService _arcDpsService;
    private DateTime? _currentVersion;
    private DateTime? _nextVersion;

    public ArcDpsUpdater(DateTime? currentVersion)
    {
        Ioc.Default.InitService(out _arcDpsService);

        _currentVersion = currentVersion;
    }

    public string GetAddonName() => "arc_dps";

    public string GetCurrentVersion() => _currentVersion?.ToString() ?? string.Empty;

    public string GetNextVersion() => _nextVersion?.ToString() ?? string.Empty;

    public void NotifyDelete() => _currentVersion = null;

    public async Task<string[]> Download()
    {
        _currentVersion = _nextVersion;

        return new[] { await _arcDpsService.Download() };
    }

    public async Task<bool> NeedsUpdate()
    {
        _nextVersion ??= await _arcDpsService.GetLatestVersion();

        if (_currentVersion is null)
            return true;

        return _currentVersion < _nextVersion;
    }
}