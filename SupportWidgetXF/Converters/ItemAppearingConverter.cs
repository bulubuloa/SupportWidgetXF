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
                var textChangedEventArgs = value as TextChangedEventArgs;
                return textChangedEventArgs;
            }
            else if (value is FocusEventArgs)
            {
                var focusEventArgs = value as FocusEventArgs;
                return focusEventArgs;
            }
            else if (value is IntegerEventArgs)
            {
                var integerEventArgs = value as IntegerEventArgs;
                return integerEventArgs.IntegerValue;
            }
            else if (value is SelectedItemChangedEventArgs)
            {
                var selectedItemChangedEventArgs = value as SelectedItemChangedEventArgs;
                return selectedItemChangedEventArgs;
            }
            else if (value is ItemVisibilityEventArgs)
            {
                var itemVisibilityEventArgs = value as ItemVisibilityEventArgs;
                return itemVisibilityEventArgs.Item;
            }
            else if (value is ObjectEventArgs)
            {
                var objectEventArgs = value as ObjectEventArgs;
                return objectEventArgs.Item;
            }
            else if (value is ItemTappedEventArgs)
            {
                var evet = value as ItemTappedEventArgs;
                return evet.Item;
            }
            else if (value is EventArgs)
            {
                var eventArgs = value as EventArgs;
                return eventArgs;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
