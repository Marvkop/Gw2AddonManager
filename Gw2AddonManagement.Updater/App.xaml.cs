using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Gw2AddonManagement.Updater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Ioc.Default.ConfigureServices();
        }
    }
}