namespace Gw2AddonManagement.Converter;

public class IsNullToStyleConverter : DependencyObject, IValueConverter
{
    public static readonly DependencyProperty StyleOnNullProperty = DependencyProperty.Register(
        nameof(StyleOnNull),
        typeof(Style),
        typeof(IsNullToStyleConverter),
        new PropertyMetadata(default(Style)));

    public static readonly DependencyProperty StyleOnNonNullProperty = DependencyProperty.Register(
        nameof(StyleOnNonNull),
        typeof(Style),
        typeof(IsNullToStyleConverter),
        new PropertyMetadata(default(Style)));

    public Style StyleOnNull
    {
        get => (Style)GetValue(StyleOnNullProperty);
        set => SetValue(StyleOnNullProperty, value);
    }

    public Style StyleOnNonNull
    {
        get => (Style)GetValue(StyleOnNonNullProperty);
        set => SetValue(StyleOnNonNullProperty, value);
    }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null or "")
        {
            return StyleOnNull;
        }

        return StyleOnNonNull;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}