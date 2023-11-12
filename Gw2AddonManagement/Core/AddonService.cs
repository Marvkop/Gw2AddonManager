using System.Threading.Tasks;
using CommunityToolkit.Mvvm.DependencyInjection;
using Gw2AddonManagement.Config;
using Gw2AddonManagement.Core.Updater;
using Gw2AddonManagement.Extensions;

namespace Gw2AddonManagement.Core;

public class AddonService
{
    private readonly FileService _fileService;
    private readonly ConfigService _configService;

    public AddonService()
    {
        Ioc.Default.InitService(out _fileService);
        Ioc.Default.InitService(out _configService);
    }

    public void RemoveAddon(IAddonUpdater updater)
    {
        var config = _configService.LoadConfig();

        var addonConfig = config.Addons[updater.GetAddonName()];

        _fileService.DeleteFile(addonConfig.File);

        config.Addons[updater.GetAddonName()] = addonConfig with { Version = null, File = null };

        _configService.SaveConfig(config);
    }

    public async Task UpdateAddon(IAddonUpdater updater)
    {
        var file = await updater.Download();

        var config = _configService.LoadConfig();
        var addonConfig = config.Addons[updater.GetAddonName()];

        config.Addons[updater.GetAddonName()] = addonConfig with { Version = updater.GetCurrentVersion(), File = file };

        _configService.SaveConfig(config);
    }
}