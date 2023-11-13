using System.Windows.Input;
using System.Windows.Media;

namespace Gw2AddonManagement.AttachedProperty;

public class EnableDragHelper
{
    public static readonly DependencyProperty EnableDragProperty = DependencyProperty.RegisterAttached(
        "EnableDrag",
        typeof(bool),
        typeof(EnableDragHelper),
        new PropertyMetadata(default(bool), OnLoaded));

    private static void OnLoaded(DependencyObject dpo, DependencyPropertyChangedEventArgs args)
    {
        if (dpo is UIElement element)
        {
            if (args.NewValue is true)
            {
                element.MouseMove += UIElementOnMouseMove;
            }
        }
    }

    private static void UIElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
    {
        if (sender is UIElement element)
        {
            if (mouseEventArgs.LeftButton == MouseButtonState.Pressed)
            {
                DependencyObject? parent = element;

                while (parent is not Window and not null)
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                (parent as Window)?.DragMove();
            }
        }
    }

    public static void SetEnableDrag(DependencyObject element, bool value)
    {
        element.SetValue(EnableDragProperty, value);
    }

    public static bool GetEnableDrag(DependencyObject element)
    {
        return (bool)element.GetValue(EnableDragProperty);
    }
}