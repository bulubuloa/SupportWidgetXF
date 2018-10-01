using System;
using System.Globalization;
using SupportWidgetXF.Models;
using Xamarin.Forms;

namespace SupportWidgetXF.Behaviors
{
    public class BehaviorsEventConverter : IValueConverter
    {
       public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TextChangedEventArgs)
            {
                var textChanged = value as TextChangedEventArgs;
                return textChanged;
            }
            else if (value is IntegerEventArgs)
            {
                var textChanged = value as IntegerEventArgs;
                return textChanged;
            }
            else if (value is MultiIntegerEventArgs)
            {
                var textChanged = value as MultiIntegerEventArgs;
                return textChanged;
            }
            else if (value is ObjectEventArgs)
            {
                var textChanged = value as ObjectEventArgs;
                return textChanged;
            }
            else if(value is ValueChangedEventArgs)
            {
                var valueChanged = value as ValueChangedEventArgs;
                return valueChanged;
            }
            else if (value is ItemVisibilityEventArgs)
            {
                var eventArgs = value as ItemVisibilityEventArgs;
                return eventArgs.Item;
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}