using System;
using System.Globalization;
using SupportWidgetXF.Models;
using Xamarin.Forms;

namespace SupportWidgetXF.Converters
{
    public class ItemAppearingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TextChangedEventArgs)
            {
                var xxx = parameter ?? "xxx";
                var textChanged = value as TextChangedEventArgs;
                return textChanged;
            }
            else if (value is FocusEventArgs)
            {
                var xxx = parameter ?? "xxx";
                var textChanged = value as FocusEventArgs;
                return textChanged;
            }
            else if (value is ItemVisibilityEventArgs)
            {
                var eventArgs = value as ItemVisibilityEventArgs;
                return eventArgs.Item;
            }
            else if (value is ItemTappedEventArgs)
            {
                var evet = value as ItemTappedEventArgs;
                return evet.Item;
            }
            else if (value is ObjectEventArgs)
            {
                var objectEventArgs = value as ObjectEventArgs;
                return objectEventArgs.Item;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
