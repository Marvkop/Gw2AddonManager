using Gw2AddonManagement.Core;
using Gw2AddonManagement.Core.Updater;
using Gw2AddonManagement.Extensions;
using Gw2AddonManagement.Messages;

namespace Gw2AddonManagement.ViewModels;

public partial class AddonViewModel : AsyncViewModel
{
    private readonly AddonService _addonService;
    private readonly IAddonUpdater _updater;

    [ObservableProperty]
    private string? _currentVersion;

    [ObservableProperty]
    private string? _latestVersion;

    [ObservableProperty]
    private bool _needsUpdate;

    public AddonViewModel(IAddonUpdater updater)
    {
        Ioc.Default.InitService(out _addonService);
        _updater = updater ?? throw new ArgumentNullException(nameof(updater));

        Name = updater.GetAddonName();
        CurrentVersion = updater.GetCurrentVersion();
    }

    public string Name { get; }

    [RelayCommand]
    private async Task RefreshNeedsUpdate()
    {
        using (StartLoading())
        {
            NeedsUpdate = await _updater.NeedsUpdate();
            LatestVersion = _updater.GetNextVersion();
        }
    }

    [RelayCommand]
    private async Task Update()
    {
        if (NeedsUpdate && !IsBusy)
        {
            using (StartLoading())
            {
                NeedsUpdate = false;

                await _addonService.UpdateAddon(_updater);

                CurrentVersion = _updater.GetCurrentVersion();
            }
        }
    }

    [RelayCommand]
    private void Remove()
    {
        if (!IsBusy)
        {
            using (StartLoading())
            {
                _addonService.RemoveAddon(_updater);

                CurrentVersion = null;
                NeedsUpdate = true;
                _updater.NotifyDelete();
            }
        }
    }

    partial void OnCurrentVersionChanged(string? value)
    {
        WeakReferenceMessenger.Default.Send<AddonStateChangedMessage>();
    }

    partial void OnLatestVersionChanged(string? value)
    {
        WeakReferenceMessenger.Default.Send<AddonStateChangedMessage>();
    }

    partial void OnNeedsUpdateChanged(bool value)
    {
        WeakReferenceMessenger.Default.Send<AddonStateChangedMessage>();
    }
}