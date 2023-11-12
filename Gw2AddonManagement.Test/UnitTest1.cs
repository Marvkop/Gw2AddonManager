using System.ComponentModel.Design;
using System.Diagnostics;
using CommunityToolkit.Mvvm.DependencyInjection;
using Gw2AddonManagement.Core;
using Gw2AddonManagement.Core.Updater;
using Gw2AddonManagement.Networking;

namespace Gw2AddonManagement.Test;

public class Tests
{
    private IAddonUpdater _sut;

    [SetUp]
    public void Setup()
    {
        var container = new ServiceContainer();

        // container.AddService(typeof(GitHubService), new GitHubService(new FileService("./")));

        Ioc.Default.ConfigureServices(container);

        _sut = new GitHubUpdater(
            SaveLocation.MainFolder,
            "arcdps_unofficial_extras_releases",
            "Krappa322",
            "arcdps_unofficial_extras_releases",
            null!);
    }

    [Test]
    public async Task Test1()
    {
        var update = await _sut.NeedsUpdate();

        Debug.WriteLine(update);

        Assert.True(update);

        await _sut.Download();
    }
}