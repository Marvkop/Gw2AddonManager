﻿using Gw2AddonManagement.Config;
using Gw2AddonManagement.Core.Updater;
using Gw2AddonManagement.Extensions;

namespace Gw2AddonManagement.Core;

public class AddonService
{
    private readonly FileService _fileService;
    private readonly ConfigService _configService;

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public AddonService()
    {
        Ioc.Default.InitService(out _fileService);
        Ioc.Default.InitService(out _configService);
    }

    public void RemoveAddon(IAddonUpdater updater)
    {
        using (_semaphore.WaitDisposable())
        {
            var config = _configService.LoadConfig();

            var addonConfig = config.Addons[updater.GetAddonName()];

            if (addonConfig.Files is not null)
            {
                foreach (var file in addonConfig.Files)
                {
                    _fileService.DeleteFile(file);
                }

                config.Addons[updater.GetAddonName()] = addonConfig with { Version = null, Files = null };
            }

            _configService.SaveConfig(config);
        }
    }

    public async Task UpdateAddon(IAddonUpdater updater)
    {
        using (await _semaphore.WaitAsyncDisposable())
        {
            var files = await updater.Download();

            var config = _configService.LoadConfig();
            var addonConfig = config.Addons[updater.GetAddonName()];

            config.Addons[updater.GetAddonName()] = addonConfig with { Version = updater.GetCurrentVersion(), Files = files };

            _configService.SaveConfig(config);
        }
    }
}