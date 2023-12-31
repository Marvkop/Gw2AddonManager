﻿using Gw2AddonManagement.ViewModels;

namespace Gw2AddonManagement.Converter;

public class CanDeleteAllConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ObservableCollection<AddonViewModel> collection)
        {
            return collection.Any(model => model.CurrentVersion is not null and not "");
        }

        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}