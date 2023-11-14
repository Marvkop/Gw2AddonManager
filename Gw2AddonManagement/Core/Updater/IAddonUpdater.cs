namespace Gw2AddonManagement.Core.Updater;

public interface IAddonUpdater
{
    public string GetAddonName();

    public string GetCurrentVersion();

    public string GetNextVersion();

    public void NotifyDelete();

    public Task<string> Download();

    public Task<bool> NeedsUpdate();
}