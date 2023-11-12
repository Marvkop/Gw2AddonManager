using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Gw2AddonManagement.ViewModels;

namespace Gw2AddonManagement.Converter;

public class CanDownloadAllConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ObservableCollection<AddonViewModel> collection)
        {
            return collection.Any(model => model.NeedsUpdate);
        }

        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}