using System;
using System.Globalization;
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
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}