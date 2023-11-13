using Gw2AddonManagement.Config;
using Gw2AddonManagement.Core;
using Gw2AddonManagement.Core.Updater;
using Gw2AddonManagement.Extensions;
using Gw2AddonManagement.Messages;

namespace Gw2AddonManagement.ViewModels;

public partial class MainWindowViewModel : ObservableObject,
    IRecipient<IsBusyMessage>,
    IRecipient<IsDoneMessage>,
    IRecipient<AddonStateChangedMessage>
{
    private readonly ConfigService _configService;
    private readonly FileService _fileService;

    private int _busyThreads;

    [ObservableProperty]
    private bool _isReady = true;

    [ObservableProperty]
    private ObservableCollection<AddonViewModel> _addons = new();

    public MainWindowViewModel()
    {
        Ioc.Default.InitService(out _configService);
        Ioc.Default.InitService(out _fileService);

        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    [RelayCommand]
    private void Initialize()
    {
        var config = _configService.LoadConfig();

        SetAddons(config.Addons.Values);
    }

    private void SetAddons(IEnumerable<Addon> addons)
    {
        Addons.Clear();

        foreach (var addon in addons)
        {
            switch (addon.Type)
            {
                case "arcdps":
                {
                    var updater = new ArcDpsUpdater(addon.Version is null ? null : DateTime.Parse(addon.Version));
                    Addons.Add(new AddonViewModel(updater));
                    break;
                }
                case "github":
                {
                    var updater = new GitHubUpdater(
                        addon.Location,
                        addon.Name,
                        addon.Metadata["owner"],
                        addon.Metadata["repo"],
                        addon.Version);
                    Addons.Add(new AddonViewModel(updater));
                    break;
                }
            }
        }
    }

    [RelayCommand]
    private void StartGw2()
    {
        Process.Start($"{_fileService.BasePath}/Gw2-64.exe");
        Application.Current.Shutdown();
    }

    [RelayCommand]
    private void CleanAddons()
    {
        var config = _configService.LoadConfig();

        foreach (var (key, addon) in config.Addons)
        {
            _fileService.DeleteFile(addon.File);
            config.Addons[key] = addon with { File = null, Version = null };
        }

        _configService.SaveConfig(config);

        SetAddons(config.Addons.Values);
    }

    [RelayCommand]
    private void DownloadAddons()
    {
        foreach (var addon in Addons)
        {
            if (addon.NeedsUpdate)
            {
                addon.UpdateCommand.Execute(null);
            }
        }
    }

    [RelayCommand]
    private void Close()
    {
        Application.Current.Shutdown();
    }

    public void Receive(IsBusyMessage _)
    {
        Interlocked.Increment(ref _busyThreads);
        IsReady = _busyThreads == 0;
    }

    public void Receive(IsDoneMessage _)
    {
        Interlocked.Decrement(ref _busyThreads);
        IsReady = _busyThreads == 0;
    }

    public void Receive(AddonStateChangedMessage _)
    {
        OnPropertyChanged(nameof(Addons));
    }
}