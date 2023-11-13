using System.ComponentModel.Design;
using CommunityToolkit.Mvvm.DependencyInjection;
using Gw2AddonManagement.Config;
using Gw2AddonManagement.Core;
using Gw2AddonManagement.Extensions;
using Gw2AddonManagement.Networking;
using Gw2AddonManagement.ViewModels;

namespace Gw2AddonManagement;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        if (!ConfigureServices())
            return;

        base.OnStartup(e);

        var mainWindow = new MainWindow
        {
            DataContext = new MainWindowViewModel()
        };

        mainWindow.Show();
    }

    private static bool ConfigureServices()
    {
        var container = new ServiceContainer();

        container.AddService<ConfigService>();
        container.AddService<FileService>();
        container.AddService<GitHubService>();
        container.AddService<ArcDpsService>();
        container.AddService<AddonService>();

        Ioc.Default.ConfigureServices(container);

        return true;
    }
}