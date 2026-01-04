using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Gw2AddonManagement.Updater;

public partial class MainWindowModel : ObservableObject
{
    [ObservableProperty]
    private double _progress;

    [RelayCommand]
    private void Initialize()
    {
    }
}