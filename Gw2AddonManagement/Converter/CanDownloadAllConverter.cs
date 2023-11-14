using Gw2AddonManagement.ViewModels;

namespace Gw2AddonManagement.Converter;

public class CanDownloadAllConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ObservableCollection<AddonViewModel> collection)
        {
            return collection.Any(model => model is { NeedsUpdate: true, Error: null });
        }

        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}